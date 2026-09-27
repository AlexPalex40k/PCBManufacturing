using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
using PCBManufacturing.Services;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PCBManufacturing.Resources.Localization;

namespace PCBManufacturing.Features.Order;

/// <summary>
/// Provides order summary state for the configured PCB.
/// </summary>
public partial class OrderViewModel : ObservableObject, IDisposable
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

    public decimal Price => _configuration.Price;

    private void OnConfigurationPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PcbConfiguration.Material):
                OnPropertyChanged(nameof(Material));
                OnPropertyChanged(nameof(Price));
                break;

            case nameof(PcbConfiguration.SolderMaskColor):
                OnPropertyChanged(nameof(SolderMask));
                break;

            case nameof(PcbConfiguration.BoardThickness):
                OnPropertyChanged(nameof(BoardThickness));
                OnPropertyChanged(nameof(Price));
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
                LocalDic.Please_complete_all_required_PCB_configuration_fields_before_placing_the_order_,
                LocalDic.Invalid_order);

            return;
        }

        var message =
            $"{LocalDic.Material}: {Material}\n" +
            $"{LocalDic.Solder_mask}: {SolderMask}\n" +
            $"{LocalDic.Board_thickness}: {BoardThickness}\n" +
            $"{LocalDic.Dimensions}: {Dimensions}\n" +
            $"{LocalDic.Layers}: {LayerCount}\n" +
            $"{LocalDic.Finish_type}: {FinishType}\n" +
            $"{LocalDic.Price}: {Price:C2}\n" +
            $"{LocalDic.Postcode}: {Postcode}";

        _dialogService.ShowInformation(message, LocalDic.Order_placed);
    }

    private bool IsOrderValid()
    {
        return _configuration.Material != null
               && _configuration.SolderMaskColor != null
               && _configuration.BoardThickness != null
               && _postcodeValidator.IsValid(_configuration.Postcode);
    }

    public void Dispose()
    {
        _configuration.PropertyChanged -= OnConfigurationPropertyChanged;
    }
}