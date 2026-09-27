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

        Parameters = SampleData.PcbParameters;
        UpdateParameters();

        ParametersView = CollectionViewSource.GetDefaultView(Parameters);

        ParametersView.GroupDescriptions.Add(
            new PropertyGroupDescription(nameof(PcbParameter.GroupKey)));

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
            case nameof(PcbConfiguration.Material):
                UpdateParameter("Material", _configuration.Material?.Name ?? "-");
                break;

            case nameof(PcbConfiguration.SolderMaskColor):
                UpdateParameter("Solder mask", _configuration.SolderMaskColor?.Name ?? "-");
                OnPropertyChanged(nameof(SolderMaskColorCode));
                break;

            case nameof(PcbConfiguration.BoardThickness):
                UpdateParameter(
                    "Board thickness",
                    _configuration.BoardThickness?.Name ?? "-");
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
                UpdateParameter("Surface finish", _configuration.FinishType);
                OnPropertyChanged(nameof(BoardDetails));
                break;
        }
    }

    private void UpdateParameters()
    {
        UpdateParameter("Width", $"{_configuration.Width} mm");
        UpdateParameter("Height", $"{_configuration.Height} mm");
        UpdateParameter("Layer count", _configuration.LayerCount.ToString());
        UpdateParameter("Material", _configuration.Material?.Name ?? "-");
        UpdateParameter("Board thickness", _configuration.BoardThickness?.Name ?? "-");
        UpdateParameter("Solder mask", _configuration.SolderMaskColor?.Name ?? "-");
        UpdateParameter("Surface finish", _configuration.FinishType);
    }

    private void UpdateParameter(string nameKey, string value)
    {
        var parameter = Parameters.FirstOrDefault(x => x.NameKey == nameKey);

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
