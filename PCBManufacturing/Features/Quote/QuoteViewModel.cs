using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using PCBManufacturing.Resources.Localization;
using PCBManufacturing.Services;

namespace PCBManufacturing.Features.Quote;

/// <summary>
/// Provides PCB manufacturing parameters and quote preview state.
/// </summary>
public partial class QuoteViewModel : ObservableValidator, IDisposable
{
    private readonly PcbConfiguration _configuration;

    private const int MinLayerCount = 1;
    private const int MaxLayerCount = 64;
    private const int MaxPreviewLayers = 20;
    private const double MinBoardDimension = 10;
    private const double MaxBoardDimension = 1000;
    private const double PreviewScale = 4;

    private double _width;
    private double _height;
    private int _layerCount;

    [ObservableProperty]
    private string _boardLabel = "PCB1";

    public QuoteViewModel(PcbConfiguration configuration)
    {
        _configuration = configuration;

        _width = configuration.Width;
        _height = configuration.Height;
        _layerCount = configuration.LayerCount;

        Parameters = new ObservableCollection<PcbParameter>(SampleData.PcbParameters);

        UpdateParameters();

        ParametersView = CollectionViewSource.GetDefaultView(Parameters);
        ParametersView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(PcbParameter.GroupKey)));

        _configuration.PropertyChanged += OnConfigurationPropertyChanged;
    }

    public ObservableCollection<PcbParameter> Parameters { get; }

    public ICollectionView ParametersView { get; }
    [Range(MinBoardDimension, MaxBoardDimension)]
    public double Width
    {
        get => _width;
        set
        {
            if (!SetProperty(ref _width, value, true))
            {
                return;
            }

            if (!GetErrors(nameof(Width)).Any())
            {
                _configuration.Width = value;
            }
        }
    }

    [Range(MinBoardDimension, MaxBoardDimension)]
    public double Height
    {
        get => _height;
        set
        {
            if (!SetProperty(ref _height, value, true))
            {
                return;
            }

            if (!GetErrors(nameof(Height)).Any())
            {
                _configuration.Height = value;
            }
        }
    }

    [Range(MinLayerCount, MaxLayerCount)]
    public int LayerCount
    {
        get => _layerCount;
        set
        {
            if (!SetProperty(ref _layerCount, value, true))
            {
                return;
            }

            if (!GetErrors(nameof(LayerCount)).Any())
            {
                _configuration.LayerCount = value;
            }
        }
    }

    public string? SolderMaskColorCode => _configuration.SolderMaskColor?.ColorCode;

    public string BoardDimensions => $"{_configuration.Width} × {_configuration.Height} mm";

    public double PreviewWidth => _configuration.Width * PreviewScale;

    public double PreviewHeight => _configuration.Height * PreviewScale;

    public string BoardDetails => $"{_configuration.LayerCount} {GetLayerWord(_configuration.LayerCount)} · " +
                                  $"{_configuration.FinishType}";

    public IEnumerable<int> PreviewLayers => Enumerable.Range(1, Math.Min(_configuration.LayerCount, MaxPreviewLayers));

    public void RefreshLocalization()
    {
        OnPropertyChanged(nameof(BoardDetails));
    }

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
                UpdateParameter("Board thickness", _configuration.BoardThickness?.Name ?? "-");
                break;

            case nameof(PcbConfiguration.Width):
                UpdateParameter("Width", $"{_configuration.Width} mm");
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(BoardDimensions));
                OnPropertyChanged(nameof(PreviewWidth));
                break;
            case nameof(PcbConfiguration.Height):
                UpdateParameter("Height", $"{_configuration.Height} mm");
                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(BoardDimensions));
                OnPropertyChanged(nameof(PreviewHeight));
                break;
            case nameof(PcbConfiguration.LayerCount):
                UpdateParameter("Layer count", _configuration.LayerCount.ToString());
                OnPropertyChanged(nameof(LayerCount));
                OnPropertyChanged(nameof(BoardDetails));
                OnPropertyChanged(nameof(PreviewLayers));
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

    private static string GetLayerWord(int count)
    {
        if (LocalizationManager.Instance.CurrentCulture.TwoLetterISOLanguageName != "ru")
        {
            return count == 1
                ? LocalDic.Layer
                : LocalDic.LayerMany;
        }

        var lastTwoDigits = count % 100;

        if (lastTwoDigits is >= 11 and <= 14)
        {
            return LocalDic.LayerMany;
        }

        return (count % 10) switch
        {
            1 => LocalDic.Layer,
            2 or 3 or 4 => LocalDic.LayerFew,
            _ => LocalDic.LayerMany
        };
    }

    public void Dispose()
    {
        _configuration.PropertyChanged -= OnConfigurationPropertyChanged;
    }
}
