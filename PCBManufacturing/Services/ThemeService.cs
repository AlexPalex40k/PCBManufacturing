using System;
using System.Windows;

namespace PCBManufacturing.Services;

/// <summary>
/// Changes application theme resources at runtime.
/// </summary>
public sealed class ThemeService : IThemeService
{
    private const string LightThemePath =
        "/PCBManufacturing;component/Resources/Themes/Colors.Light.xaml";

    private const string DarkThemePath =
        "/PCBManufacturing;component/Resources/Themes/Colors.Dark.xaml";

    /// <inheritdoc />
    public void ApplyLightTheme()
    {
        ApplyTheme(LightThemePath);
    }

    /// <inheritdoc />
    public void ApplyDarkTheme()
    {
        ApplyTheme(DarkThemePath);
    }

    private static void ApplyTheme(string source)
    {
        var dictionaries =
            Application.Current.Resources.MergedDictionaries;

        if (dictionaries.Count == 0)
        {
            return;
        }

        dictionaries[0] = new ResourceDictionary
        {
            Source = new Uri(source, UriKind.RelativeOrAbsolute)
        };
    }
}