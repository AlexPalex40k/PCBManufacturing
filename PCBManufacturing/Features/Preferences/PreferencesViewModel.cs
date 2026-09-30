using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
using PCBManufacturing.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using PCBManufacturing.Resources.Localization;

namespace PCBManufacturing.Features.Preferences;

/// <summary>
/// Provides data and state for configuring PCB manufacturing preferences.
/// </summary>
public partial class PreferencesViewModel : ObservableValidator
{
    private readonly PcbConfiguration _configuration;
    private readonly IConfigurationStorage _configurationStorage;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PriceModifier))]
    [NotifyPropertyChangedFor(nameof(ProductionDays))]
    private Material? _selectedMaterial;

    [ObservableProperty]
    private SolderMaskColor? _selectedSolderMaskColor;

    [ObservableProperty]
    private BoardThickness? _selectedBoardThickness;

    //TODO ALEX check DataAnnotations validation
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(
        ErrorMessageResourceType = typeof(LocalDic),
        ErrorMessageResourceName = "Postcode_is_required")]
    [RegularExpression(
        @"^\d{4,10}$",
        ErrorMessageResourceType = typeof(LocalDic),
        ErrorMessageResourceName = "Postcode_must_contain_4_to_10_digits")]
    private string _postcode = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="PreferencesViewModel"/> class
    /// with predefined PCB manufacturing options.
    /// </summary>
    public PreferencesViewModel(
        PcbConfiguration configuration,
        IConfigurationStorage configurationStorage)
    {
        _configuration = configuration;
        _configurationStorage = configurationStorage;

        Materials = SampleData.Materials;
        SolderMaskColors = SampleData.SolderMaskColors;
        BoardThicknesses = SampleData.BoardThicknesses;

        LoadConfiguration();
    }

    public ObservableCollection<Material> Materials { get; }

    public ObservableCollection<SolderMaskColor> SolderMaskColors { get; }

    public ObservableCollection<BoardThickness> BoardThicknesses { get; }

    public decimal PriceModifier => SelectedMaterial?.PriceModifier ?? 1.0m;

    public int ProductionDays => SelectedMaterial?.ProductionDays ?? 0;

    /// <summary>
    /// Re-evaluates localized validation messages after the UI culture changes.
    /// </summary>
    public void RefreshValidation()
    {
        ValidateProperty(Postcode, nameof(Postcode));
    }

    /// <summary>
    /// Saves the current PCB preferences to persistent storage.
    /// </summary>
    public void SaveConfiguration()
    {
        var data = new PcbConfigurationData
        {
            MaterialName = SelectedMaterial?.Name,
            SolderMaskColorName = SelectedSolderMaskColor?.Name,
            BoardThickness = SelectedBoardThickness?.Millimeters,
            Postcode = Postcode,
            Width = _configuration.Width,
            Height = _configuration.Height,
            LayerCount = _configuration.LayerCount
        };

        _configurationStorage.Save(data);
    }

    //TODO ALEX check generated partial hooks
    partial void OnSelectedMaterialChanged(Material? value)
    {
        _configuration.Material = value;
    }

    partial void OnSelectedSolderMaskColorChanged(SolderMaskColor? value)
    {
        _configuration.SolderMaskColor = value;
    }

    partial void OnSelectedBoardThicknessChanged(BoardThickness? value)
    {
        _configuration.BoardThickness = value;
    }

    partial void OnPostcodeChanged(string value)
    {
        _configuration.Postcode = value;
    }

    private void LoadConfiguration()
    {
        var data = _configurationStorage.Load();

        SelectedMaterial =
            Materials.FirstOrDefault(x => x.Name == data?.MaterialName)
            ?? Materials.FirstOrDefault();

        SelectedSolderMaskColor =
            SolderMaskColors.FirstOrDefault(x => x.Name == data?.SolderMaskColorName)
            ?? SolderMaskColors.FirstOrDefault();

        SelectedBoardThickness =
            BoardThicknesses.FirstOrDefault(x => x.Millimeters == data?.BoardThickness)
            ?? BoardThicknesses.FirstOrDefault();

        Postcode = data?.Postcode ?? string.Empty;

        if (data?.Width is double width && IsValidDimension(width))
        {
            _configuration.Width = width;
        }

        if (data?.Height is double height && IsValidDimension(height))
        {
            _configuration.Height = height;
        }

        if (data?.LayerCount is > 0)
        {
            _configuration.LayerCount = data.LayerCount.Value;
        }
    }

    private static bool IsValidDimension(double value)
    {
        return double.IsFinite(value) && value > 0;
    }
}
