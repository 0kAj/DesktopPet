using System.Windows.Threading;

namespace DesktopPet;

[Obsolete("DispatcherTimer is tied to the UI message queue, which delays ticks and cannot provide stable or high-precision timing. Use BetterTickingWindow instead.", false)]
public abstract class TickingWindow : TimedWindow
{
    private readonly DispatcherTimer _timer;
    
    protected TickingWindow()
    {
        DoTick = true;

        _timer = new DispatcherTimer();
        SetDelta(10);
        _timer.Tick += (_, __) => Tick();
    }

    private bool DoTick { get; set; }

    public override bool IsTicking => _timer.IsEnabled;

    protected void SetDelta(double delta)
    {
        _timer.Interval = TimeSpan.FromMilliseconds(delta);
    }

    public override void StartTicking()
    {
        _timer.Start();
        OnTickStart();
    }

    public override void StopTicking()
    {
        _timer.Stop();
        OnTickStop();
    }
}