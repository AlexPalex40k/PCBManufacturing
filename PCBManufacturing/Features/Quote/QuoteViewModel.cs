using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PCBManufacturing.Features.Quote;

/// <summary>
/// Provides PCB manufacturing parameters and quote preview state.
/// </summary>
public partial class QuoteViewModel : ObservableObject, IDisposable
{
    private readonly PcbConfiguration _configuration;

    private const double PreviewScale = 4;

    [ObservableProperty]
    private string _boardLabel = "PCB1";

    public QuoteViewModel(PcbConfiguration configuration)
    {
        _configuration = configuration;

        Parameters = new ObservableCollection<PcbParameter>
        {
            new("Board", "Width", $"{_configuration.Width} mm"),
            new("Board", "Height", $"{_configuration.Height} mm"),
            new("Board", "Layer count", _configuration.LayerCount.ToString()),

            new("Fabrication", "Material", _configuration.Material?.Name ?? "-"),
            new("Fabrication", "Board thickness",
                _configuration.BoardThickness?.Name ?? "-"),
            new("Fabrication", "Solder mask",
                _configuration.SolderMaskColor?.Name ?? "-"),
            new("Fabrication", "Surface finish", _configuration.FinishType)
        };

        ParametersView = CollectionViewSource.GetDefaultView(Parameters);

        ParametersView.GroupDescriptions.Add(
            new PropertyGroupDescription(nameof(PcbParameter.Group)));

        _configuration.PropertyChanged += OnConfigurationPropertyChanged;
    }

    public ObservableCollection<PcbParameter> Parameters { get; }

    public ICollectionView ParametersView { get; }


    public string? SolderMaskColorCode => _configuration.SolderMaskColor?.ColorCode;

    public string BoardDimensions => $"{_configuration.Width} × {_configuration.Height} mm";

    public string BoardDetails => $"{_configuration.LayerCount} Layers · {_configuration.FinishType}";

    public double PreviewWidth => _configuration.Width * PreviewScale;

    public double PreviewHeight => _configuration.Height * PreviewScale;

    private void OnConfigurationPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PcbConfiguration.SolderMaskColor):
                UpdateParameter("Solder mask", _configuration.SolderMaskColor?.Name ?? "-");
                OnPropertyChanged(nameof(SolderMaskColorCode));
                break;

            case nameof(PcbConfiguration.Width):
                UpdateParameter("Width", $"{_configuration.Width} mm");
                OnPropertyChanged(nameof(BoardDimensions));
                OnPropertyChanged(nameof(PreviewWidth));
                break;

            case nameof(PcbConfiguration.Height):
                UpdateParameter("Height", $"{_configuration.Height} mm");
                OnPropertyChanged(nameof(BoardDimensions));
                OnPropertyChanged(nameof(PreviewHeight));
                break;

            case nameof(PcbConfiguration.LayerCount):
                UpdateParameter("Layer count", _configuration.LayerCount.ToString());
                OnPropertyChanged(nameof(BoardDetails));
                break;

            case nameof(PcbConfiguration.FinishType):
                UpdateParameter("Finish type", _configuration.FinishType);
                OnPropertyChanged(nameof(BoardDetails));
                break;
        }
    }

    private void UpdateParameter(string name, string value)
    {
        var parameter = Parameters.FirstOrDefault(x => x.Name == name);

        if (parameter == null)
        {
            return;
        }

        var index = Parameters.IndexOf(parameter);

        Parameters[index] = parameter with
        {
            Value = value
        };
    }

    public void Dispose()
    {
        _configuration.PropertyChanged -= OnConfigurationPropertyChanged;
    }
}
