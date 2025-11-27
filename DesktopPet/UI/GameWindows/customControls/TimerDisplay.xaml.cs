using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DesktopPet.MiniGames;

namespace DesktopPet.UI.GameWindows.customControls;

public partial class TimerDisplay : UserControl
{
    public TimerDisplay()
    {
        Timer = new TimerViewModel();
        DataContext = Timer;
        Timer.PropertyChanged += ViewModelOnPropertyChanged;
        Timer.Tick += () =>
        {
            if (Timer.Remaining > 10) return;
            AnimateBounce();
        };
        InitializeComponent();
    }

    public TimerViewModel Timer { get; }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        AnimateFontSize(Timer.TimerFontSize);
    }

    private void AnimateFontSize(double target)
    {
        var anim = new DoubleAnimation
        {
            To = target,
            Duration = TimeSpan.FromMilliseconds(200),
            EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        };

        Timer_Tb.BeginAnimation(TextBlock.FontSizeProperty, anim);
    }

    private void AnimateBounce()
    {
        var bounceUp = new DoubleAnimation
        {
            To = 1.25,
            Duration = TimeSpan.FromMilliseconds(80),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var bounceDown = new DoubleAnimation
        {
            To = 1.0,
            Duration = TimeSpan.FromMilliseconds(120),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
        };

        bounceUp.Completed += (_, _) =>
        {
            ScaleT.BeginAnimation(ScaleTransform.ScaleXProperty, bounceDown);
            ScaleT.BeginAnimation(ScaleTransform.ScaleYProperty, bounceDown);
        };

        ScaleT.BeginAnimation(ScaleTransform.ScaleXProperty, bounceUp);
        ScaleT.BeginAnimation(ScaleTransform.ScaleYProperty, bounceUp);
    }
}