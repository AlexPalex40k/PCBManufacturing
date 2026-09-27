using PCBManufacturing.Features.Order;
using PCBManufacturing.Models;
using PCBManufacturing.Services;
using PCBManufacturing.Tests.Helpers;

namespace PCBManufacturing.Tests.Features;

public sealed class OrderViewModelTests
{
    [Fact]
    public void PlaceOrderCommand_WithValidConfiguration_ShowsConfirmation()
    {
        var configuration = CreateValidConfiguration();
        var dialogService = new FakeDialogService();
        var postcodeValidator = new PostcodeValidator();

        var viewModel = new OrderViewModel(
            configuration,
            dialogService,
            postcodeValidator);

        viewModel.PlaceOrderCommand.Execute(null);

        Assert.NotNull(dialogService.InformationMessage);
        Assert.Null(dialogService.ErrorMessage);
    }

    private static PcbConfiguration CreateValidConfiguration()
    {
        return new PcbConfiguration
        {
            Material = new Material("FR-4", 1.0m, 5, 20),
            SolderMaskColor = new SolderMaskColor("Green", "#008C4A", 30),
            BoardThickness = new BoardThickness("1.6 mm", 1.6, 1.2m),
            Postcode = "11000"
        };
    }

    [Fact]
    public void PlaceOrderCommand_WithInvalidPostcode_ShowsError()
    {
        var configuration = CreateValidConfiguration();
        configuration.Postcode = "abc";

        var dialogService = new FakeDialogService();
        var postcodeValidator = new PostcodeValidator();

        var viewModel = new OrderViewModel(
            configuration,
            dialogService,
            postcodeValidator);

        viewModel.PlaceOrderCommand.Execute(null);

        Assert.NotNull(dialogService.ErrorMessage);
        Assert.Null(dialogService.InformationMessage);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, true)]
    public void PlaceOrderCommand_WithMissingRequiredConfiguration_ShowsError(
        bool hasMaterial,
        bool hasSolderMask,
        bool hasThickness)
    {
        var configuration = CreateValidConfiguration();

        if (!hasMaterial)
        {
            configuration.Material = null;
        }

        if (!hasSolderMask)
        {
            configuration.SolderMaskColor = null;
        }

        if (!hasThickness)
        {
            configuration.BoardThickness = null;
        }

        var dialogService = new FakeDialogService();
        var postcodeValidator = new PostcodeValidator();

        var viewModel = new OrderViewModel(
            configuration,
            dialogService,
            postcodeValidator);

        viewModel.PlaceOrderCommand.Execute(null);

        Assert.NotNull(dialogService.ErrorMessage);
        Assert.Null(dialogService.InformationMessage);
    }

    [Fact]
    public void Price_DependsOnMaterialAndBoardThickness()
    {
        var configuration = new PcbConfiguration
        {
            Material = new Material(
                "Aluminum",
                1.3m,
                7,
                30m),

            BoardThickness = new BoardThickness(
                "1.6 mm",
                1.6,
                1.08m)
        };

        Assert.Equal(124.96m, configuration.Price);
    }
}