using System.Runtime.InteropServices;
using System.Windows;

namespace DesktopPet;

public class Helper
{
    // Source - https://stackoverflow.com/questions/4226740/how-do-i-get-the-current-mouse-screen-coordinates-in-wpf
    // Posted by Fredrik Hedblad
    // Retrieved 2025-11-06, License - CC BY-SA 4.0

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetCursorPos(ref Win32Point pt);

    public static Point GetMousePosition()
    {
        var w32Mouse = new Win32Point();
        GetCursorPos(ref w32Mouse);

        return new Point(w32Mouse.X, w32Mouse.Y);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Point
    {
        public Int32 X;
        public Int32 Y;
    }
}