using System.Collections.ObjectModel;
using PCBManufacturing.Models;

namespace PCBManufacturing;

/// <summary>
/// Provides predefined data used by the application.
/// </summary>
public static class SampleData
{
    public static double PcbWidth => 100;
    public static double PcbHeight => 80;
    public static int PcbLayerCount => 4;
    public static string PcbFinishType => "HASL";
    public static decimal PcbFinishPrice => 6m;

    public static ObservableCollection<Material> Materials { get; } = new()
        {
            new("FR-4", 1.0m, 5, 20m),
            new("Aluminum", 1.3m, 7, 30m),
            new("Rogers", 1.8m, 10, 45m)
        };

    public static ObservableCollection<SolderMaskColor> SolderMaskColors { get; } = new()
        {
            new("Green", "#008C4A", 3m),
            new("Red", "#C43C3C", 5m),
            new("Blue", "#3978B8", 5m)
        };

    public static ObservableCollection<BoardThickness> BoardThicknesses { get; } = new()
        {
            new("0.8 mm", 0.8, 5m),
            new("1.6 mm", 1.6, 8m),
            new("2.0 mm", 2.0, 12m)
        };
}
