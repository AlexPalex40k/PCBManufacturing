namespace PCBManufacturing.Services;

/// <summary>
/// Provides operations for changing the application color theme.
/// </summary>
public interface IThemeService
{
    /// <summary>
    /// Applies the light application theme.
    /// </summary>
    void ApplyLightTheme();

    /// <summary>
    /// Applies the dark application theme.
    /// </summary>
    void ApplyDarkTheme();
}