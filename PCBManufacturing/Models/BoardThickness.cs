namespace PCBManufacturing.Models;

/// <summary>
/// Represents an available PCB thickness option.
/// </summary>
/// <param name="Name">The display name of the thickness.</param>
/// <param name="Millimeters">The board thickness in millimeters.</param>
public sealed record BoardThickness(
    string Name,
    double Millimeters);