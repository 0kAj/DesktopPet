using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopPet.WPF.Converter;

public class StringToColorConverter : IValueConverter
{
    public virtual object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string colorStr)
        {
           return ColorConverter.ConvertFromString(colorStr);
        }
        return Colors.Black;
    }

    public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Color color ? color.ToString() : Colors.Black.ToString();
    }
}