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
    private bool _isUpdatingParameters;

    private const double PreviewScale = 4;


    [ObservableProperty]
    private string _boardLabel = "PCB1";

    public QuoteViewModel(PcbConfiguration configuration)
    {
        _configuration = configuration;

        Parameters = new ObservableCollection<QuoteParameterViewModel>
        {
            new(
                QuoteParameterType.Width,
                "Board",
                "Width (mm)",
                FormatNumber(_configuration.Width),
                _configuration.WidthPrice,
                true),

            new(
                QuoteParameterType.Height,
                "Board",
                "Height (mm)",
                FormatNumber(_configuration.Height),
                _configuration.HeightPrice,
                true),

            new(
                QuoteParameterType.LayerCount,
                "Board",
                "Layer count",
                _configuration.LayerCount.ToString(),
                _configuration.LayerCountPrice,
                true),

            new(
                QuoteParameterType.Material,
                "Fabrication",
                "Material",
                _configuration.Material?.Name ?? "-",
                _configuration.MaterialPrice,
                false),

            new(
                QuoteParameterType.BoardThickness,
                "Fabrication",
                "Board thickness",
                _configuration.BoardThickness?.Name ?? "-",
                _configuration.BoardThicknessPrice,
                false),

            new(
                QuoteParameterType.SolderMask,
                "Fabrication",
                "Solder mask",
                _configuration.SolderMaskColor?.Name ?? "-",
                _configuration.SolderMaskPrice,
                false),

            new(
                QuoteParameterType.SurfaceFinish,
                "Fabrication",
                "Surface finish",
                _configuration.FinishType,
                _configuration.FinishPrice,
                false)
        };

        foreach (var parameter in Parameters.Where(x => x.IsEditable))
        {
            parameter.PropertyChanged += OnParameterPropertyChanged;
        }

        ParametersView = CollectionViewSource.GetDefaultView(Parameters);

        _configuration.PropertyChanged += OnConfigurationPropertyChanged;
    }

    public ObservableCollection<QuoteParameterViewModel> Parameters { get; }

    public ICollectionView ParametersView { get; }

    public string? SolderMaskColorCode => _configuration.SolderMaskColor?.ColorCode;

    public string BoardDimensions => $"{_configuration.Width} × {_configuration.Height} mm";

    public string BoardDetails => $"{_configuration.LayerCount} Layers · {_configuration.FinishType}";

    public double PreviewWidth => _configuration.Width * PreviewScale;

    public double PreviewHeight => _configuration.Height * PreviewScale;

    public decimal TotalPrice => _configuration.Price;


    private void OnParameterPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (_isUpdatingParameters
            || e.PropertyName != nameof(QuoteParameterViewModel.Value)
            || sender is not QuoteParameterViewModel parameter)
        {
            return;
        }

        switch (parameter.Type)
        {
            case QuoteParameterType.Width:
                if (double.TryParse(parameter.Value, out var width)
                    && width > 0)
                {
                    _configuration.Width = width;
                }

                break;

            case QuoteParameterType.Height:
                if (double.TryParse(parameter.Value, out var height)
                    && height > 0)
                {
                    _configuration.Height = height;
                }

                break;

            case QuoteParameterType.LayerCount:
                if (int.TryParse(parameter.Value, out var layerCount)
                    && layerCount > 0)
                {
                    _configuration.LayerCount = layerCount;
                }

                break;
        }
    }

    private void OnConfigurationPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PcbConfiguration.Width):
                UpdateParameter(
                    QuoteParameterType.Width,
                    FormatNumber(_configuration.Width),
                    _configuration.WidthPrice);

                OnPropertyChanged(nameof(BoardDimensions));
                OnPropertyChanged(nameof(PreviewWidth));
                break;

            case nameof(PcbConfiguration.Height):
                UpdateParameter(
                    QuoteParameterType.Height,
                    FormatNumber(_configuration.Height),
                    _configuration.HeightPrice);

                OnPropertyChanged(nameof(BoardDimensions));
                OnPropertyChanged(nameof(PreviewHeight));
                break;

            case nameof(PcbConfiguration.LayerCount):
                UpdateParameter(
                    QuoteParameterType.LayerCount,
                    _configuration.LayerCount.ToString(),
                    _configuration.LayerCountPrice);

                OnPropertyChanged(nameof(BoardDetails));
                break;

            case nameof(PcbConfiguration.Material):
                UpdateParameter(
                    QuoteParameterType.Material,
                    _configuration.Material?.Name ?? "-",
                    _configuration.MaterialPrice);
                break;

            case nameof(PcbConfiguration.BoardThickness):
                UpdateParameter(
                    QuoteParameterType.BoardThickness,
                    _configuration.BoardThickness?.Name ?? "-",
                    _configuration.BoardThicknessPrice);
                break;

            case nameof(PcbConfiguration.SolderMaskColor):
                UpdateParameter(
                    QuoteParameterType.SolderMask,
                    _configuration.SolderMaskColor?.Name ?? "-",
                    _configuration.SolderMaskPrice);

                OnPropertyChanged(nameof(SolderMaskColorCode));
                break;

            case nameof(PcbConfiguration.FinishType):
                UpdateParameter(
                    QuoteParameterType.SurfaceFinish,
                    _configuration.FinishType,
                    _configuration.FinishPrice);

                OnPropertyChanged(nameof(BoardDetails));
                break;

            case nameof(PcbConfiguration.Price):
                OnPropertyChanged(nameof(TotalPrice));
                break;
        }
    }

    private static string FormatNumber(double value)
    {
        return value.ToString("0.##");
    }

    private void UpdateParameter(
        QuoteParameterType type,
        string value,
        decimal price)
    {
        var parameter =
            Parameters.FirstOrDefault(x => x.Type == type);

        if (parameter == null)
        {
            return;
        }

        _isUpdatingParameters = true;

        try
        {
            parameter.Value = value;
            parameter.Price = price;
        }
        finally
        {
            _isUpdatingParameters = false;
        }
    }

    public void Dispose()
    {
        _configuration.PropertyChanged -= OnConfigurationPropertyChanged;
    }
}
