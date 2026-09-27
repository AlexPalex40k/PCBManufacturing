namespace PCBManufacturing.Models;

/// <summary>
/// Represents an available PCB thickness option.
/// </summary>
/// <param name="Name">The display name of the thickness.</param>
/// <param name="Millimeters">The board thickness in millimeters.</param>
/// <param name="Price">The multiplier applied to the base PCB price.</param>
public sealed record BoardThickness(
    string Name,
    double Millimeters,
    decimal Price);