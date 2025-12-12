using System.Globalization;
using System.Windows.Data;

namespace DesktopPet.WPF.Converter;

public class DoubleScaleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double input) return null;
        
        var scale = 2.0; // default: input doubled cause it is a "double" converter xD
        if (parameter != null)
            scale = double.TryParse(parameter.ToString(), NumberStyles.Any, culture, out double result) ? result : scale;
        
        return input * scale;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double input) return null;
        
        var scale = 2.0; // default: input doubled cause it is a "double" converter xD
        if (parameter != null)
            scale = double.TryParse(parameter.ToString(), NumberStyles.Any, culture, out double result) ? result : scale;
        
        return input / scale;
    }
}