using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Features.Preferences;
using PCBManufacturing.Features.Quote;

namespace PCBManufacturing.ViewModels;

/// <summary>
/// Provides application-level state for the main window.
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(
        PreferencesViewModel preferences, 
        QuoteViewModel quote)
    {
        Preferences = preferences;
        Quote = quote;
    }

    public PreferencesViewModel Preferences { get; }

    public QuoteViewModel Quote { get; }
}