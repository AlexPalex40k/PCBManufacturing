using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;

namespace PCBManufacturing.Features.Preferences;

/// <summary>
/// Provides data and state for configuring PCB manufacturing preferences.
/// </summary>
public partial class PreferencesViewModel : ObservableObject
{
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

    [ObservableProperty]
    private Material? _selectedMaterial;

    [ObservableProperty]
    private SolderMaskColor? _selectedSolderMaskColor;

    [ObservableProperty]
    private BoardThickness? _selectedBoardThickness;

    [ObservableProperty]
    private string _postcode = string.Empty;
}