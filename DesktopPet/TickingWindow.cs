using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesktopPet;

public abstract class TickingWindow : Window
{
    private readonly DispatcherTimer _timer;

    protected TickingWindow()
    {
        DoTick = true;

        _timer = new DispatcherTimer();
        SetDelta(0.01);
        _timer.Tick += (_, __) => Tick();
    }

    private bool DoTick { get; set; }

    public bool IsTicking => _timer.IsEnabled;

    protected void SetDelta(double delta)
    {
        _timer.Interval = TimeSpan.FromSeconds(delta);
    }

    protected void StartTicking()
    {
        _timer.Start();
    }

    protected void StopTicking()
    {
        _timer.Stop();
    }

    protected abstract void Tick();

    protected Point GetGlobalMousePos()
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


    protected double GetWindowsScale(Visual v)
    {
        return VisualTreeHelper.GetDpi(v).PixelsPerDip;
    }

    public Vector GetPositionVector()
    {
        return new Vector(Left + Width / 2, Top);
    }
}