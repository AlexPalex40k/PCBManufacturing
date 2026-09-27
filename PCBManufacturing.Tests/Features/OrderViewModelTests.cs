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
            Material = new Material("FR-4", 1.0m, 5),
            SolderMaskColor = new SolderMaskColor("Green", "#008C4A"),
            BoardThickness = new BoardThickness("1.6 mm", 1.6),
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
    public void TotalPrice_WithValidConfiguration_ReturnsExpectedValue()
    {
        var configuration = new PcbConfiguration
        {
            Material = new Material("FR-4", 1.0m, 5),
            BoardThickness = new BoardThickness("0.8 mm", 0.8),
            Width = 100,
            Height = 80,
            LayerCount = 4
        };

        var viewModel = new OrderViewModel(
            configuration,
            new FakeDialogService(),
            new PostcodeValidator());

        Assert.Equal(236.80m, viewModel.TotalPrice);
    }
}