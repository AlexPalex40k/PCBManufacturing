using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Features.Order;
using PCBManufacturing.Features.Preferences;
using PCBManufacturing.Features.Quote;
using PCBManufacturing.Services;
using System.Globalization;

namespace PCBManufacturing.ViewModels;

/// <summary>
/// Provides application-level state for the main window.
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    private readonly IThemeService _themeService;
    private readonly IPcbConfigurationService _configurationService;

    [ObservableProperty]
    private bool _isDarkTheme;

    [ObservableProperty]
    private bool _isRussian;

    public MainWindowViewModel(
        IPcbConfigurationService configurationService,
        IThemeService themeService,
        PreferencesViewModel preferencesVm,
        QuoteViewModel quoteVm,
        OrderViewModel orderVm)
    {
        _configurationService = configurationService;

        _themeService = themeService;

        PreferencesVm = preferencesVm;
        QuoteVm = quoteVm;
        OrderVm = orderVm;
    }

    public PreferencesViewModel PreferencesVm { get; }
    public QuoteViewModel QuoteVm { get; }
    public OrderViewModel OrderVm { get; }

    /// <summary>
    /// Saves application state before shutdown.
    /// </summary>
    public void SaveState()
    {
        _configurationService.Save();
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

    partial void OnIsRussianChanged(bool value)
    {
        var culture = value
            ? CultureInfo.GetCultureInfo("ru")
            : CultureInfo.GetCultureInfo("en");

        LocalizationManager.Instance.SetCulture(culture);
        PreferencesVm.RefreshValidation();
    }
}
