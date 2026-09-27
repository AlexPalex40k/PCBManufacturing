using System.Globalization;
using System.Windows.Data;
using PCBManufacturing.Services;

namespace PCBManufacturing.Converters;

/// <summary>
/// Converts a localization resource key to the localized text.
/// </summary>
public sealed class LocalizationConverter : IMultiValueConverter
{
    public object Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        if (values.Length == 0 ||
            values[0] is not string key ||
            string.IsNullOrWhiteSpace(key))
        {
            return string.Empty;
        }

        return LocalizationManager.Instance[key];
    }

    public object[] ConvertBack(
        object value,
        Type[] targetTypes,
        object parameter,
        CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}