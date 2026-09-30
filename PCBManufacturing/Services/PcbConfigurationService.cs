using PCBManufacturing.Models;

namespace PCBManufacturing.Services;

/// <summary>
/// Loads and saves the PCB configuration.
/// </summary>
public sealed class PcbConfigurationService : IPcbConfigurationService
{
    private readonly PcbConfiguration _configuration;
    private readonly IConfigurationStorage _configurationStorage;

    public PcbConfigurationService(
        PcbConfiguration configuration,
        IConfigurationStorage configurationStorage)
    {
        _configuration = configuration;
        _configurationStorage = configurationStorage;
    }

    public void Load()
    {
        var data = _configurationStorage.Load();

        if (data == null)
        {
            ApplyDefaults();
            return;
        }

        _configuration.Material = _configuration.Material;
        _configuration.SolderMaskColor = _configuration.SolderMaskColor;
        _configuration.BoardThickness = _configuration.BoardThickness;
        _configuration.Postcode = data.Postcode;
        _configuration.Width = data.Width;
        _configuration.Height = data.Height;
        _configuration.LayerCount = data.LayerCount;
    }

    public void Save()
    {
        var data = new PcbConfigurationData
        {
            MaterialName = _configuration.Material?.Name,
            SolderMaskColorName = _configuration.SolderMaskColor?.Name,
            BoardThickness = _configuration.BoardThickness?.Millimeters,
            Postcode = _configuration.Postcode,

            Width = _configuration.Width,
            Height = _configuration.Height,
            LayerCount = _configuration.LayerCount
        };

        _configurationStorage.Save(data);
    }

    private void ApplyDefaults()
    {
        _configuration.Material =
            SampleData.Materials.FirstOrDefault();

        _configuration.SolderMaskColor =
            SampleData.SolderMaskColors.FirstOrDefault();

        _configuration.BoardThickness =
            SampleData.BoardThicknesses.FirstOrDefault();

        _configuration.Width = SampleData.PcbWidth;
        _configuration.Height = SampleData.PcbHeight;
        _configuration.LayerCount = SampleData.PcbLayerCount;
    }
}