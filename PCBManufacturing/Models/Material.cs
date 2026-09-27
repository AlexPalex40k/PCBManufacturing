namespace PCBManufacturing.Models;

/// <summary>
/// Represents a PCB base material and its impact on manufacturing cost and production time.
/// </summary>
/// <param name="Name">The display name of the material.</param>
/// <param name="PriceModifier">The multiplier applied to the base PCB price.</param>
/// <param name="ProductionDays">The estimated number of days required for production.</param>
public sealed record Material(
    string Name,
    decimal PriceModifier,
    int ProductionDays,
    decimal Price);