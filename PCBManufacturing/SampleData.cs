using System.Collections.ObjectModel;
using PCBManufacturing.Models;

namespace PCBManufacturing;

/// <summary>
/// Provides predefined data used by the application.
/// </summary>
public static class SampleData
{
    public static ObservableCollection<Material> Materials { get; } = new()
        {
            new("FR-4", 1.0m, 5),
            new("Aluminum", 1.3m, 7),
            new("Rogers", 1.8m, 10)
        };

    public static ObservableCollection<SolderMaskColor> SolderMaskColors { get; } = new()
        {
            new("Green", "#008C4A"),
            new("Red", "#C43C3C"),
            new("Blue", "#3978B8")
        };

    public static ObservableCollection<BoardThickness> BoardThicknesses { get; } = new()
        {
            new("0.8 mm", 0.8),
            new("1.6 mm", 1.6),
            new("2.0 mm", 2.0)
        };

    public static double PcbWidth => 100;
    public static double PcbHeight => 80;
    public static int PcbLayerCount => 4;
    public static string PcbFinishType => "HASL";
}