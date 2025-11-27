using System.Windows;

namespace DesktopPet;

public abstract class HighPrecisionTickingWindow : TimedWindow
{
    HighPrecisionTimer _timer;
    
    private uint _timeMillis = 10;

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

        try
        {
            _timer.StartTicking();
        }
        catch (Exception e)
        {
            var result = MessageBox.Show(e.Message,
                "Fatal-Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error,
                MessageBoxResult.OK);
            if (result == MessageBoxResult.OK)
                Close();
        }

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
        Dispatcher.BeginInvoke(() =>
        {
            Tick(deltaMillis);
            Tick();
        });
    }
}