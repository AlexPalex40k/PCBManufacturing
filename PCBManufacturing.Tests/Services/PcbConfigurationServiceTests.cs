using PCBManufacturing.Models;
using PCBManufacturing.Services;

namespace PCBManufacturing.Tests.Services;

public sealed class PcbConfigurationServiceTests
{
    [Fact]
    public void Save_PersistsCurrentConfiguration()
    {
        var configuration = new PcbConfiguration
        {
            Material = SampleData.Materials[1],
            SolderMaskColor = SampleData.SolderMaskColors[2],
            BoardThickness = SampleData.BoardThicknesses[1],
            SurfaceFinish = SampleData.SurfaceFinishes[2],
            Postcode = "11000",
            Width = 125.5,
            Height = 95,
            LayerCount = 8
        };
        var storage = new InMemoryConfigurationStorage();
        var service = new PcbConfigurationService(configuration, storage);

        service.Save();

        var saved = Assert.IsType<PcbConfigurationData>(storage.SavedConfiguration);
        Assert.Equal(configuration.Material.Name, saved.MaterialName);
        Assert.Equal(configuration.SolderMaskColor.Name, saved.SolderMaskColorName);
        Assert.Equal(configuration.BoardThickness.Millimeters, saved.BoardThickness);
        Assert.Equal(configuration.SurfaceFinish.Name, saved.SurfaceFinishName);
        Assert.Equal(configuration.Postcode, saved.Postcode);
        Assert.Equal(configuration.Width, saved.Width);
        Assert.Equal(configuration.Height, saved.Height);
        Assert.Equal(configuration.LayerCount, saved.LayerCount);
    }

    [Fact]
    public void Load_WithSavedConfiguration_RestoresCurrentConfiguration()
    {
        var configuration = new PcbConfiguration();
        var storage = new InMemoryConfigurationStorage
        {
            ConfigurationToLoad = new PcbConfigurationData
            {
                MaterialName = SampleData.Materials[2].Name,
                SolderMaskColorName = SampleData.SolderMaskColors[1].Name,
                BoardThickness = SampleData.BoardThicknesses[2].Millimeters,
                SurfaceFinishName = SampleData.SurfaceFinishes[3].Name,
                Postcode = "12345",
                Width = 140,
                Height = 90.5,
                LayerCount = 10
            }
        };
        var service = new PcbConfigurationService(configuration, storage);

        service.Load();

        Assert.Same(SampleData.Materials[2], configuration.Material);
        Assert.Same(SampleData.SolderMaskColors[1], configuration.SolderMaskColor);
        Assert.Same(SampleData.BoardThicknesses[2], configuration.BoardThickness);
        Assert.Same(SampleData.SurfaceFinishes[3], configuration.SurfaceFinish);
        Assert.Equal("12345", configuration.Postcode);
        Assert.Equal(140, configuration.Width);
        Assert.Equal(90.5, configuration.Height);
        Assert.Equal(10, configuration.LayerCount);
    }

    [Fact]
    public void Load_WithoutSavedConfiguration_AppliesDefaults()
    {
        var configuration = new PcbConfiguration
        {
            Material = null,
            SolderMaskColor = null,
            BoardThickness = null,
            Width = 1,
            Height = 1,
            LayerCount = 1
        };
        var service = new PcbConfigurationService(
            configuration,
            new InMemoryConfigurationStorage());

        service.Load();

        Assert.Same(SampleData.Materials[0], configuration.Material);
        Assert.Same(SampleData.SolderMaskColors[0], configuration.SolderMaskColor);
        Assert.Same(SampleData.BoardThicknesses[0], configuration.BoardThickness);
        Assert.Same(SampleData.SurfaceFinishes[0], configuration.SurfaceFinish);
        Assert.Equal(SampleData.PcbWidth, configuration.Width);
        Assert.Equal(SampleData.PcbHeight, configuration.Height);
        Assert.Equal(SampleData.PcbLayerCount, configuration.LayerCount);
    }

    private sealed class InMemoryConfigurationStorage : IConfigurationStorage
    {
        public PcbConfigurationData? ConfigurationToLoad { get; init; }

        public PcbConfigurationData? SavedConfiguration { get; private set; }

        public PcbConfigurationData? Load()
        {
            return ConfigurationToLoad;
        }

        public void Save(PcbConfigurationData configuration)
        {
            SavedConfiguration = configuration;
        }
    }
}
