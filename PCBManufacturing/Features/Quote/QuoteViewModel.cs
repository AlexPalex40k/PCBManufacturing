using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PCBManufacturing.Features.Quote;

/// <summary>
/// Provides PCB manufacturing parameters and quote preview state.
/// </summary>
public partial class QuoteViewModel : ObservableObject, IDisposable
{
    private readonly PcbConfiguration _configuration;

    public QuoteViewModel(PcbConfiguration configuration)
    {
        _configuration = configuration;

        Parameters = new ObservableCollection<PcbParameter>
        {
            new(
                "Dimensions",
                "Width",
                $"{_configuration.Width} mm"),

            new(
                "Dimensions",
                "Height",
                $"{_configuration.Height} mm"),

            new(
                "Layers",
                "Layer count",
                _configuration.LayerCount.ToString()),

            new(
                "Finish",
                "Finish type",
                _configuration.FinishType)
        };

        _configuration.PropertyChanged += OnConfigurationPropertyChanged;
    }

    public ObservableCollection<PcbParameter> Parameters { get; }

    public string? SolderMaskColorCode => _configuration.SolderMaskColor?.ColorCode;

    public string BoardDimensions => $"{_configuration.Width} × {_configuration.Height} mm";

    public string BoardDetails => $"{_configuration.LayerCount} Layers · {_configuration.FinishType}";

    private void OnConfigurationPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PcbConfiguration.SolderMaskColor):
                OnPropertyChanged(nameof(SolderMaskColorCode));
                break;

            case nameof(PcbConfiguration.Width):
            case nameof(PcbConfiguration.Height):
                OnPropertyChanged(nameof(BoardDimensions));
                break;

            case nameof(PcbConfiguration.LayerCount):
            case nameof(PcbConfiguration.FinishType):
                OnPropertyChanged(nameof(BoardDetails));
                break;
        }
    }

    public void Dispose()
    {
        _configuration.PropertyChanged -= OnConfigurationPropertyChanged;
    }
}