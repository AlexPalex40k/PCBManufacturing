using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Features.Order;
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
        QuoteViewModel quote, 
        OrderViewModel order)
    {
        Preferences = preferences;
        Quote = quote;
        Order = order;
    }

    public PreferencesViewModel Preferences { get; }

    public QuoteViewModel Quote { get; }
    public OrderViewModel Order { get; }
}