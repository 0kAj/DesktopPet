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
        _timer.Start(); //Todo Start this at any window manually
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

    public Point GetDPISaveGlobalMousePos()
    {
        var pos = GetGlobalMousePos();
        var scale = GetWindowsScale(this); // z. B. 1.0 oder 1.25

        // physische Pixel → WPF Device-Independent Pixels (DIPs)
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