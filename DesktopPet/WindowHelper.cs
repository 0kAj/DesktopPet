using System.Windows;

namespace DesktopPet;

public static class WindowHelper
{
    public static void FitToScreen(Window window)
    {
        var workArea = SystemParameters.WorkArea;
        window.Left = workArea.Left;
        window.Top = workArea.Top;
        window.Width = workArea.Width;
        window.Height = workArea.Height;
    }
}