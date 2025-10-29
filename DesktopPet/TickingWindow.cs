using System.Windows;
using System.Windows.Threading;

namespace DesktopPet;

public abstract class TickingWindow : Window
{
    private bool DoTick { get; set; }

    public bool IsTicking => _timer.IsEnabled;
    private DispatcherTimer _timer;

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
}