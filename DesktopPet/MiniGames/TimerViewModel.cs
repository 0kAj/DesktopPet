using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopPet.MiniGames;

public partial class TimerViewModel : ObservableObject
{
    private readonly DispatcherTimer _timer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayTime))]
    [NotifyPropertyChangedFor(nameof(TimerColor))]
    [NotifyPropertyChangedFor(nameof(TimerFontSize))]
    private int _remaining;

    public TimerViewModel(int startSeconds = 30)
    {
        Remaining = startSeconds;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        _timer.Tick += (_, _) =>
        {
            Remaining--;
            Tick?.Invoke();
            if (Remaining == 0)
            {
                _timer.Stop();
                Timeout?.Invoke();
            }
        };
    }

    public int TimerFontSize => Remaining switch
    {
        <= 5 => 80,
        <= 10 => 70,
        <= 20 => 60,
        _ => 50
    };

    public Brush TimerColor => Remaining switch
    {
        <= 5 => Brushes.Red,
        <= 10 => Brushes.OrangeRed,
        <= 20 => Brushes.Orange,
        _ => Brushes.White
    };

    public string DisplayTime =>
        TimeSpan.FromSeconds(Remaining).ToString(@"mm\:ss");

    public event Action? Tick;

    public event Action? Timeout;

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

    public void AddRemaining(int seconds)
    {
        Remaining = Math.Max(0, Remaining + seconds);
    }
}