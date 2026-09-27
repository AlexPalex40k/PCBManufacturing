namespace PCBManufacturing.Models;

/// <summary>
/// Represents an available solder mask color.
/// </summary>
/// <param name="Name">The display name of the color.</param>
/// <param name="ColorCode">The hexadecimal color code used for PCB visualization.</param>
public sealed record SolderMaskColor(
    string Name,
    string ColorCode,
    decimal Price);