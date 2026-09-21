using CommunityToolkit.Mvvm.ComponentModel;

namespace PCBManufacturing.Models;

/// <summary>
/// Represents the current PCB manufacturing configuration shared across application features.
/// </summary>
public partial class PcbConfiguration : ObservableObject
{
    [ObservableProperty]
    private Material? _material;

    [ObservableProperty]
    private SolderMaskColor? _solderMaskColor;

    [ObservableProperty]
    private BoardThickness? _boardThickness;

    [ObservableProperty]
    private string _postcode = string.Empty;

    [ObservableProperty]
    private double _width = 100;

    [ObservableProperty]
    private double _height = 80;

    [ObservableProperty]
    private int _layerCount = 4;

    [ObservableProperty]
    private string _finishType = "HASL";
}