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
        viewModel.PropertyChanged += (_, args) =>
            changedProperties.Add(args.PropertyName);

        configuration.SolderMaskColor =
            new SolderMaskColor("Blue", "#2563EB");

        Assert.Equal("#2563EB", viewModel.SolderMaskColorCode);
        Assert.Contains(nameof(QuoteViewModel.SolderMaskColorCode), changedProperties);
        Assert.Contains(viewModel.Parameters, parameter => parameter.Name == "Solder mask" && parameter.Value == "Blue");
    }
}
