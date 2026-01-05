using System.Windows;

namespace DesktopPet.Engine.GameObjects;

public abstract class HighPrecisionTickingGameObject : TimedGameObject
{
    private uint _timeMillis = 10;
    private HighPrecisionTimer? _timer;

    public override bool IsTicking => _timer == null ? false : _timer.IsTicking;

    public void SetDelta(uint deltaMillis)
    {
        StopTicking();
        _timeMillis = deltaMillis;
        StartTicking();
    }

    public override void StartTicking()
    {
        if (_timer == null)
        {
            _timer = new HighPrecisionTimer();
            _timer.Tick += TimerTick;
        }

        _timer.Interval = _timeMillis;

        _timer.StartTicking();

        OnTickStart();
    }

    public override void StopTicking()
    {
        if (_timer == null)
            return;

        _timer.StopTicking();

        OnTickStop();
    }

    private void TimerTick(float deltaMillis)
    {
        if (Application.Current == null)
        {
            StopTicking();
            return;
        }
        
        Application.Current.Dispatcher.BeginInvoke(() =>
        {
            Tick(deltaMillis);
            Tick();
        });
    }
}