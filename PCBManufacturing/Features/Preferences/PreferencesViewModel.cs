using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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

    //TODO ALEX check DataAnnotations validation
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Postcode is required.")]
    [RegularExpression(@"^\d{4,10}$", ErrorMessage = "Postcode must contain 4 to 10 digits.")]
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

        SelectedMaterial = Materials.FirstOrDefault();
        SelectedSolderMaskColor = SolderMaskColors.FirstOrDefault();
        SelectedBoardThickness = BoardThicknesses.FirstOrDefault();
    }

    public ObservableCollection<Material> Materials { get; }

    public ObservableCollection<SolderMaskColor> SolderMaskColors { get; }

    public ObservableCollection<BoardThickness> BoardThicknesses { get; }

    public decimal PriceModifier => SelectedMaterial?.PriceModifier ?? 1.0m;

    public int ProductionDays => SelectedMaterial?.ProductionDays ?? 0;

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
}