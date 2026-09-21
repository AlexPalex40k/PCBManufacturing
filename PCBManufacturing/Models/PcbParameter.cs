namespace PCBManufacturing.Models;

/// <summary>
/// Represents a PCB parameter displayed in the manufacturing quote.
/// </summary>
public sealed record PcbParameter(
    string Group,
    string Name,
    string Value);