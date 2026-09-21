using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Features.Preferences;

namespace PCBManufacturing.ViewModels;

/// <summary>
/// Provides application-level state for the main window.
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(PreferencesViewModel preferences)
    {
        Preferences = preferences;
    }

    public PreferencesViewModel Preferences { get; }
}