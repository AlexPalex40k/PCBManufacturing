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
    public PreferencesViewModel()
    {
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
}