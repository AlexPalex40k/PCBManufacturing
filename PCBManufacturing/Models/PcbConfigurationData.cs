namespace PCBManufacturing.Models;

/// <summary>
/// Represents PCB configuration data stored between application sessions.
/// </summary>
public sealed class PcbConfigurationData
{
    public string? MaterialName { get; set; }

    public string? SolderMaskColorName { get; set; }

    public double? BoardThickness { get; set; }

    public string? SurfaceFinishName { get; set; }

    public string Postcode { get; set; } = string.Empty;

    public double Width { get; set; }

    public double Height { get; set; }

    public int LayerCount { get; set; }
}
