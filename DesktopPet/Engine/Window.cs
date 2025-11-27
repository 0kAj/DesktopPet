using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopPet.Engine;

public class Window : System.Windows.Window
{
    private Point GetGlobalMousePos()
    {
        return PointToScreen(Mouse.GetPosition(this));
    }

    public Point GetDpiSaveGlobalMousePos()
    {
        var pos = GetGlobalMousePos();
        var scale = GetWindowsScale(this); // windows-Scale-Fac 1.0, 1.25, ...

        // pixelpos to dpi save pos
        return new Point(pos.X / scale, pos.Y / scale);
    }


    private double GetWindowsScale(Visual v)
    {
        return VisualTreeHelper.GetDpi(v).PixelsPerDip;
    }

    public Vector GetPositionVector()
    {
        return new Vector(Left + Width / 2, Top);
    }
}