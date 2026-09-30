using PCBManufacturing.Features.Preferences;
using PCBManufacturing.Models;

namespace PCBManufacturing.Tests.Features;

public sealed class PreferencesViewModelTests
{
    [Fact]
    public void Constructor_UsesCurrentConfiguration()
    {
        var configuration = new PcbConfiguration
        {
            Material = SampleData.Materials[1],
            SolderMaskColor = SampleData.SolderMaskColors[2],
            BoardThickness = SampleData.BoardThicknesses[1],
            Postcode = "11000"
        };

        var viewModel = new PreferencesViewModel(configuration);

        Assert.Same(configuration.Material, viewModel.SelectedMaterial);
        Assert.Same(configuration.SolderMaskColor, viewModel.SelectedSolderMaskColor);
        Assert.Same(configuration.BoardThickness, viewModel.SelectedBoardThickness);
        Assert.Equal(configuration.Postcode, viewModel.Postcode);
    }

    [Fact]
    public void PreferencesChanged_UpdatesCurrentConfiguration()
    {
        var configuration = new PcbConfiguration();
        var viewModel = new PreferencesViewModel(configuration);

        viewModel.SelectedMaterial = SampleData.Materials[2];
        viewModel.SelectedSolderMaskColor = SampleData.SolderMaskColors[1];
        viewModel.SelectedBoardThickness = SampleData.BoardThicknesses[2];
        viewModel.Postcode = "12345";

        Assert.Same(viewModel.SelectedMaterial, configuration.Material);
        Assert.Same(viewModel.SelectedSolderMaskColor, configuration.SolderMaskColor);
        Assert.Same(viewModel.SelectedBoardThickness, configuration.BoardThickness);
        Assert.Equal(viewModel.Postcode, configuration.Postcode);
    }
}
