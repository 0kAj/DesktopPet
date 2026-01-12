using System.Windows;
using System.Windows.Controls;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.Utils;

public static class SizingHelper
{
    public static void FitToScreen(Window window)
    {
        var workArea = SystemParameters.WorkArea;
        window.Left = workArea.Left;
        window.Top = workArea.Top;
        window.Width = workArea.Width;
        window.Height = workArea.Height;
    }

    public static void FitToScreen(UserControl control)
    {
        var workArea = SystemParameters.WorkArea;
        control.Width = workArea.Width;
        control.Height = workArea.Height;
    }
}