using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PriceModifier))]
    [NotifyPropertyChangedFor(nameof(ProductionDays))]
    private Material? _selectedMaterial;

    [ObservableProperty]
    private SolderMaskColor? _selectedSolderMaskColor;

    [ObservableProperty]
    private BoardThickness? _selectedBoardThickness;

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
    public PreferencesViewModel(PcbConfiguration configuration)
    {
        _configuration = configuration;

        Materials = SampleData.Materials;
        SolderMaskColors = SampleData.SolderMaskColors;
        BoardThicknesses = SampleData.BoardThicknesses;

        SelectedMaterial = _configuration.Material ?? Materials.FirstOrDefault();
        SelectedBoardThickness = _configuration.BoardThickness ?? BoardThicknesses.FirstOrDefault();
        SelectedSolderMaskColor = _configuration.SolderMaskColor ?? SolderMaskColors.FirstOrDefault();
        Postcode = _configuration.Postcode;
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
}
