namespace PCBManufacturing.Services;

/// <summary>
/// Provides application dialog operations without coupling view models to WPF.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Displays an informational message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="title">The dialog title.</param>
    void ShowInformation(string message, string title);

    /// <summary>
    /// Displays an error message to the user.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="title">The dialog title.</param>
    void ShowError(string message, string title);
}