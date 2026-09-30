using PCBManufacturing.Features.Quote;
using PCBManufacturing.Models;

namespace PCBManufacturing.Tests.Features;

public sealed class QuoteViewModelTests
{
    [Fact]
    public void SolderMaskColorChanged_UpdatesPreviewColor()
    {
        var configuration = new PcbConfiguration
        {
            SolderMaskColor = new SolderMaskColor("Green", "#008C4A")
        };

        using var viewModel = new QuoteViewModel(configuration);
        var changedProperties = new List<string?>();
        viewModel.PropertyChanged += (_, args) => changedProperties.Add(args.PropertyName);

        configuration.SolderMaskColor = new SolderMaskColor("Blue", "#2563EB");

        Assert.Equal("#2563EB", viewModel.SolderMaskColorCode);
        Assert.Contains(nameof(QuoteViewModel.SolderMaskColorCode), changedProperties);
        Assert.Contains(viewModel.Parameters, parameter => parameter.NameKey == "Solder mask" && parameter.Value == "Blue");
    }

    [Fact]
    public void BoardValuesChanged_UpdatesSharedConfigurationAndPreview()
    {
        var configuration = new PcbConfiguration();

        using var viewModel = new QuoteViewModel(configuration);

        viewModel.Width = 120.5;
        viewModel.Height = 75;
        viewModel.LayerCount = 6;

        Assert.Equal(120.5, configuration.Width);
        Assert.Equal(75, configuration.Height);
        Assert.Equal(6, configuration.LayerCount);
        Assert.Equal(
            $"{configuration.Width} × {configuration.Height} mm",
            viewModel.BoardDimensions);
        Assert.Equal(482, viewModel.PreviewWidth);
        Assert.Equal(300, viewModel.PreviewHeight);
        Assert.Contains(
            viewModel.Parameters,
            parameter => parameter.NameKey == "Layer count" && parameter.Value == "6");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    public void WidthChanged_WithInvalidValue_IsRejected(double value)
    {
        using var viewModel = new QuoteViewModel(new PcbConfiguration());

        Assert.Throws<ArgumentOutOfRangeException>(() => viewModel.Width = value);
    }
}
