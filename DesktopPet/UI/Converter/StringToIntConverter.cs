using System.Globalization;
using System.Windows.Data;

namespace DesktopPet.UI.Converter;

public class StringToIntConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return int.TryParse(value?.ToString(), out int result) ? result : 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() ?? "0";
    }
}