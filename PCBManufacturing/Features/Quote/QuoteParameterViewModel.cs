using CommunityToolkit.Mvvm.ComponentModel;
using PCBManufacturing.Models;

namespace PCBManufacturing.Features.Quote;
    public partial class QuoteParameterViewModel : ObservableObject
    {
        public QuoteParameterViewModel(
            QuoteParameterType type,
            string group,
            string name,
            string value,
            decimal price,
            bool isEditable)
        {
            Type = type;
            Group = group;
            Name = name;
            _value = value;
            _price = price;
            IsEditable = isEditable;
        }

        public QuoteParameterType Type { get; }

        public string Group { get; }

        public string Name { get; }

        public bool IsEditable { get; }

        [ObservableProperty]
        private string _value;

        [ObservableProperty]
        private decimal _price;
    }
