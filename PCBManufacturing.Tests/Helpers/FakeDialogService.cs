using PCBManufacturing.Services;

namespace PCBManufacturing.Tests.Helpers;

internal sealed class FakeDialogService : IDialogService
{
    public string? InformationMessage { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void ShowInformation(string message, string title)
    {
        InformationMessage = message;
    }

    public void ShowError(string message, string title)
    {
        ErrorMessage = message;
    }
}