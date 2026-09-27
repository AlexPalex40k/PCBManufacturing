using System.Windows;

namespace PCBManufacturing.Services;

/// <summary>
/// Displays dialogs using standard WPF message boxes.
/// </summary>
public sealed class DialogService : IDialogService
{
    /// <inheritdoc />
    public void ShowInformation(string message, string title)
    {
        MessageBox.Show(
            message,
            title,
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    /// <inheritdoc />
    public void ShowError(string message, string title)
    {
        MessageBox.Show(
            message,
            title,
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}