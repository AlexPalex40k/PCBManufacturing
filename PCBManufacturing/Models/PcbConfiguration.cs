using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing;
using PCBManufacturing.Models;
using Material = PCBManufacturing.Models.Material;

public partial class PcbConfiguration : ObservableObject
{
    private const decimal DimensionPricePerMillimeter = 0.04m;
    private const decimal LayerPrice = 2.50m;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MaterialPrice))]
    [NotifyPropertyChangedFor(nameof(Price))]
    private Material? _material;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SolderMaskPrice))]
    [NotifyPropertyChangedFor(nameof(Price))]
    private SolderMaskColor? _solderMaskColor;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BoardThicknessPrice))]
    [NotifyPropertyChangedFor(nameof(Price))]
    private BoardThickness? _boardThickness;

    [ObservableProperty]
    private string _postcode = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WidthPrice))]
    [NotifyPropertyChangedFor(nameof(Price))]
    private double _width = SampleData.PcbWidth;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HeightPrice))]
    [NotifyPropertyChangedFor(nameof(Price))]
    private double _height = SampleData.PcbHeight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LayerCountPrice))]
    [NotifyPropertyChangedFor(nameof(Price))]
    private int _layerCount = SampleData.PcbLayerCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FinishPrice))]
    [NotifyPropertyChangedFor(nameof(Price))]
    private string _finishType = SampleData.PcbFinishType;

    public decimal WidthPrice =>
        Math.Round((decimal)Width * DimensionPricePerMillimeter, 2);

    public decimal HeightPrice =>
        Math.Round((decimal)Height * DimensionPricePerMillimeter, 2);

    public decimal LayerCountPrice => LayerCount * LayerPrice;

    public decimal MaterialPrice => Material?.Price ?? 0m;

    public decimal BoardThicknessPrice => BoardThickness?.Price ?? 0m;

    public decimal SolderMaskPrice => SolderMaskColor?.Price ?? 0m;

    public decimal FinishPrice => SampleData.PcbFinishPrice;

    public decimal Price =>
        WidthPrice
        + HeightPrice
        + LayerCountPrice
        + MaterialPrice
        + BoardThicknessPrice
        + SolderMaskPrice
        + FinishPrice;
}