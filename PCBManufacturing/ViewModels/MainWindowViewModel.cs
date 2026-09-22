using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Features.Order;
using PCBManufacturing.Features.Preferences;
using PCBManufacturing.Features.Quote;
using PCBManufacturing.Services;

namespace PCBManufacturing.ViewModels;

/// <summary>
/// Provides application-level state for the main window.
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    private readonly IThemeService _themeService;
    
    [ObservableProperty]
    private bool _isDarkTheme;

    public MainWindowViewModel(
        PreferencesViewModel preferences, 
        QuoteViewModel quote, 
        OrderViewModel order,
        IThemeService themeService)
    {
        Preferences = preferences;
        Quote = quote;
        Order = order;
        _themeService = themeService;
    }

    public PreferencesViewModel Preferences { get; }

    public QuoteViewModel Quote { get; }
    public OrderViewModel Order { get; }

    /// <summary>
    /// Saves application state before shutdown.
    /// </summary>
    public void SaveState()
    {
        Preferences.SaveConfiguration();
    }

    partial void OnIsDarkThemeChanged(bool value)
    {
        if (value)
        {
            _themeService.ApplyDarkTheme();
        }
        else
        {
            _themeService.ApplyLightTheme();
        }
    }
}