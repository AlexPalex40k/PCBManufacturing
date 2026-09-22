using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
using PCBManufacturing.Services;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PCBManufacturing.Features.Order;

/// <summary>
/// Provides order summary state for the configured PCB.
/// </summary>
public partial class OrderViewModel : ObservableObject
{
    private readonly PcbConfiguration _configuration;
    private readonly IDialogService _dialogService;
    private readonly IPostcodeValidator _postcodeValidator;

    public OrderViewModel(
        PcbConfiguration configuration,
        IDialogService dialogService,
        IPostcodeValidator postcodeValidator)
    {
        _configuration = configuration;
        _dialogService = dialogService;
        _postcodeValidator = postcodeValidator;

        _configuration.PropertyChanged += OnConfigurationPropertyChanged;
    }

    public string Material =>
        _configuration.Material?.Name ?? string.Empty;

    public string SolderMask =>
        _configuration.SolderMaskColor?.Name ?? string.Empty;

    public string BoardThickness =>
        _configuration.BoardThickness?.Name ?? string.Empty;

    public string Dimensions => $"{_configuration.Width} × {_configuration.Height} mm";

    public string LayerCount => _configuration.LayerCount.ToString();

    public string FinishType => _configuration.FinishType;

    public string Postcode => _configuration.Postcode;

    private void OnConfigurationPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PcbConfiguration.Material):
                OnPropertyChanged(nameof(Material));
                break;

            case nameof(PcbConfiguration.SolderMaskColor):
                OnPropertyChanged(nameof(SolderMask));
                break;

            case nameof(PcbConfiguration.BoardThickness):
                OnPropertyChanged(nameof(BoardThickness));
                break;

            case nameof(PcbConfiguration.Width):
            case nameof(PcbConfiguration.Height):
                OnPropertyChanged(nameof(Dimensions));
                break;

            case nameof(PcbConfiguration.LayerCount):
                OnPropertyChanged(nameof(LayerCount));
                break;

            case nameof(PcbConfiguration.FinishType):
                OnPropertyChanged(nameof(FinishType));
                break;

            case nameof(PcbConfiguration.Postcode):
                OnPropertyChanged(nameof(Postcode));
                break;
        }
    }

    [RelayCommand]
    private void PlaceOrder()
    {
        if (!IsOrderValid())
        {
            _dialogService.ShowError(
                "Please complete all required PCB configuration fields before placing the order.",
                "Invalid Order");

            return;
        }

        var message =
            $"Material: {Material}\n" +
            $"Solder mask: {SolderMask}\n" +
            $"Board thickness: {BoardThickness}\n" +
            $"Dimensions: {Dimensions}\n" +
            $"Layers: {LayerCount}\n" +
            $"Finish: {FinishType}\n" +
            $"Postcode: {Postcode}";

        _dialogService.ShowInformation(
            message,
            "Order Placed");
    }

    private bool IsOrderValid()
    {
        return _configuration.Material != null
               && _configuration.SolderMaskColor != null
               && _configuration.BoardThickness != null
               && _postcodeValidator.IsValid(_configuration.Postcode);
    }
}