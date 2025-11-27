using System.Globalization;
using System.Windows.Data;

namespace DesktopPet.UI.Converter;

public class StringToDoubleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return double.TryParse(value?.ToString(), out var d) ? d : 0.0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() ?? "0";
    }
}