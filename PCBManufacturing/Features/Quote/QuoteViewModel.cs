using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;

namespace PCBManufacturing.Features.Quote;

/// <summary>
/// Provides PCB manufacturing parameters and quote preview state.
/// </summary>
public partial class QuoteViewModel : ObservableObject
{
    public QuoteViewModel()
    {
        Parameters = SampleData.PcbParameters;
    }

    public ObservableCollection<PcbParameter> Parameters { get; }
}