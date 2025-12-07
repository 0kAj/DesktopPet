using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DesktopPet.Data.Attributes;
using DesktopPet.Handlers;
using DesktopPet.Utils;
using PauseMenu = DesktopPet.WPF.GameWindows.customControls.UI.PauseMenu;

namespace DesktopPet.Engine;

public abstract class MiniGameWindow : HighPrecisionTickingWindow
{
    protected readonly PetBrain Brain;
    private bool _isPaused;

    private PauseMenu? _pauseMenu;

    private Label? _fpsDisplay;

    protected MiniGameWindow(PetBrain brain)
    {
        Brain = brain;
        Brain.PetWindow.KeyDown += TogglePauseMenu;

        Loaded += (_, _) =>
        {
            // create FPS-display
            _fpsDisplay = new Label
            {
                FontSize = 50,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Chartreuse,
                Visibility = Visibility.Hidden,
                ContentStringFormat = "FPS: {0:F0}"
            };
            MiniGameUiCanvas.Children.Add(_fpsDisplay);
        };

        KeyDown += (_, e) =>
        {
            if (_fpsDisplay == null) return;
            //toggable debug fps
            if (e.Key == Key.Tab)
            {
                var vis = _fpsDisplay.Visibility switch
                {
                    Visibility.Visible => Visibility.Hidden,
                    _ => Visibility.Visible
                };

                _fpsDisplay.Visibility = vis;
            }
        };


        PetAttributeHelper.InitAttributes(Brain, out var collectedFood, out var collectedThirst);
        CollectedFoodAttribute = collectedFood;
        CollectedThirstAttribute = collectedThirst;

        DataContext = this;
    }

    public abstract string GameName { get; }
    protected abstract Canvas MiniGameUiCanvas { get; }

    private PetAttribute CollectedFoodAttribute { get; }
    private PetAttribute CollectedThirstAttribute { get; }

    protected int CollectedFood
    {
        get => int.TryParse(CollectedFoodAttribute.Value, out var val) ? val : 0;
        set => CollectedFoodAttribute.Value = value.ToString();
    }

    protected int CollectedThirst
    {
        get => int.TryParse(CollectedThirstAttribute.Value, out var val) ? val : 0;
        set => CollectedThirstAttribute.Value = value.ToString();
    }


    public abstract void Start();

    protected virtual void End()
    {
        Brain.PetWindow.KeyDown -= TogglePauseMenu;
    }

    private void TogglePauseMenu(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Escape) return;

        if (_isPaused)
            HidePauseMenu();
        else
            ShowPauseMenu();
    }

    protected override void Tick(float delta)
    {
        if (_fpsDisplay == null) return;

        _fpsDisplay.Content = 1000f / delta;
    }

    private void ShowPauseMenu()
    {
        StopTicking();
        Brain.PetWindow.StopTicking();
        _isPaused = true;

        if (_pauseMenu == null)
        {
            _pauseMenu = (PauseMenu)FindResource("PauseMenuControl");
            _pauseMenu.ResumeClicked += HidePauseMenu;
            _pauseMenu.LeaveClicked += () =>
            {
                Brain.PetWindow.StartTicking();
                End();
            };
            MiniGameUiCanvas.Children.Add(_pauseMenu);

            SizingHelper.FitToScreen(_pauseMenu);

            Canvas.SetLeft(_pauseMenu, (MiniGameUiCanvas.ActualWidth - _pauseMenu.Width) / 2);
            Canvas.SetTop(_pauseMenu, (MiniGameUiCanvas.ActualHeight - _pauseMenu.Height) / 2);
        }

        _pauseMenu.Visibility = Visibility.Visible;
    }

    private void HidePauseMenu()
    {
        if (_pauseMenu != null)
            _pauseMenu.Visibility = Visibility.Collapsed;
        StartTicking();
        Brain.PetWindow.StartTicking();
        _isPaused = false;
    }
}