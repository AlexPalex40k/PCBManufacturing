using System.ComponentModel;
using System.Globalization;
using PCBManufacturing.Resources.Localization;

namespace PCBManufacturing.Services;

/// <summary>
/// Provides localized application strings and manages the current UI culture.
/// </summary>
public sealed class LocalizationManager : INotifyPropertyChanged
{
    private static readonly LocalizationManager _instance = new();

    private CultureInfo _currentCulture = CultureInfo.GetCultureInfo("en");

    private LocalizationManager()
    {
    }

    /// <summary>
    /// Gets the shared localization manager instance.
    /// </summary>
    public static LocalizationManager Instance => _instance;

    /// <summary>
    /// Gets the currently selected application culture.
    /// </summary>
    public CultureInfo CurrentCulture => _currentCulture;

    /// <summary>
    /// Gets a localized string for the specified resource key.
    /// </summary>
    /// <param name="key">The resource key.</param>
    /// <returns>The localized string.</returns>
    public string this[string key]
    {
        get
        {
            return LocalDic.ResourceManager.GetString(
                       key,
                       _currentCulture)
                   ?? key;
        }
    }

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Changes the current application culture.
    /// </summary>
    /// <param name="culture">The culture to apply.</param>
    public void SetCulture(CultureInfo culture)
    {
        if (_currentCulture.Name == culture.Name)
        {
            return;
        }

        _currentCulture = culture;

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs("Item[]"));

        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(nameof(CurrentCulture)));
    }
}