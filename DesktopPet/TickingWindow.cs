using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesktopPet;

public abstract class TickingWindow : Window
{
    private bool DoTick { get; set; }

    public bool IsTicking => _timer.IsEnabled;
    private readonly DispatcherTimer _timer;

    protected TickingWindow()
    {
        DoTick = true;
        
        _timer = new DispatcherTimer();
        SetDelta(0.01);
        _timer.Tick += (_, __) => Tick();
        _timer.Start();
    }

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
    
    protected Point GetGlobalMousePos() => PointToScreen(Mouse.GetPosition(this));
    
    public Point GetDPISaveGlobalMousePos()
    {
        var pos = GetGlobalMousePos();
        double scale = GetWindowsScale(this); // z. B. 1.0 oder 1.25

        // physische Pixel → WPF Device-Independent Pixels (DIPs)
        return new Point(pos.X / scale, pos.Y / scale);
    }

    
    protected double GetWindowsScale(Visual v) => VisualTreeHelper.GetDpi(v).PixelsPerDip;
    
}