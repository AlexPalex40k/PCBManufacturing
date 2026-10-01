namespace PCBManufacturing.Models;

/// <summary>
/// Represents a PCB surface finish and its impact on manufacturing cost.
/// </summary>
/// <param name="Name">The display name of the surface finish.</param>
/// <param name="PriceModifier">The multiplier applied to the base PCB price.</param>
public sealed record SurfaceFinish(
    string Name,
    decimal PriceModifier);


