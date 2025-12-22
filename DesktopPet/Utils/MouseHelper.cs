using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopPet.Utils;

public class MouseHelper
{
    private static Point GetGlobalMousePos(Visual visual, IInputElement inputElement)
    {
        return visual.PointToScreen(Mouse.GetPosition(inputElement));
    }

    private static double GetWindowsScale(Visual v)
    {
        return VisualTreeHelper.GetDpi(v).PixelsPerDip;
    }

    public static Point GetDpiSaveGlobalMousePos(Visual visual, IInputElement inputElement)
    {
        var pos = GetGlobalMousePos(visual, inputElement);
        var scale = GetWindowsScale(visual); // windows-Scale-Fac 1.0, 1.25, ...

        // pixelpos to dpi save pos
        return new Point(pos.X / scale, pos.Y / scale);
    }
}