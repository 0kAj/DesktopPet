using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesktopPet.MiniGames;

public class TimerViewModel : INotifyPropertyChanged
{
    private readonly DispatcherTimer _timer;
    private int _remaining;
    public Action Tick;

    public Action Timeout;

    public TimerViewModel(int startSeconds = 30)
    {
        Remaining = startSeconds;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        _timer.Tick += (_, _) =>
        {
            Tick?.Invoke();
            if (Remaining == 0)
            {
                _timer.Stop();
                Timeout?.Invoke();
                return;
            }

            Remaining--;
        };
    }

    public int Remaining
    {
        get => _remaining;
        set
        {
            _remaining = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(DisplayTime));
            OnPropertyChanged(nameof(TimerColor));
            OnPropertyChanged(nameof(TimerFontSize));
        }
    }

    public int TimerFontSize
    {
        get
        {
            return Remaining switch
            {
                <= 5 => 80,
                <= 10 => 70,
                <= 20 => 60,
                _ => 50
            };
        }
    }

    public Brush TimerColor
    {
        get
        {
            return Remaining switch
            {
                <= 5 => Brushes.Red,
                <= 10 => Brushes.OrangeRed,
                <= 20 => Brushes.Orange,
                _ => Brushes.White
            };
        }
    }

    public string DisplayTime =>
        TimeSpan.FromSeconds(Remaining).ToString(@"mm\:ss");

    public event PropertyChangedEventHandler? PropertyChanged;

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    public void Set(int seconds)
    {
        Remaining = seconds;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddRemaining(int seconds)
    {
        Remaining += seconds;
    }
}