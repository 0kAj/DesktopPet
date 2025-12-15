using System.Globalization;
using System.Windows.Media;

namespace DesktopPet.WPF.Converter;

public class StringToColorBrushConverter : StringToColorConverter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var color = base.Convert(value, targetType, parameter, culture);
        if (color is Color c)
            return new SolidColorBrush(c);
        
        return new SolidColorBrush(Colors.Black);
    }
}