using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DesktopPet.Data.Attributes;
using DesktopPet.Handlers;
using DesktopPet.UI.GameWindows.customControls;
using DesktopPet.Utils;

namespace DesktopPet.Engine;

public abstract class MiniGameWindow : HighPrecisionTickingWindow
{
    protected readonly PetBrain _brain;
    private bool _isPaused;

    private PauseMenu _pauseMenu;

    private Label fpsDisplay;

    protected MiniGameWindow(PetBrain brain)
    {
        _brain = brain;
        brain.PetWindow.KeyDown += TogglePauseMenu;

        Loaded += (_, _) =>
        {
            // create FPS-display
            fpsDisplay = new Label();
            fpsDisplay.FontSize = 50;
            fpsDisplay.FontWeight = FontWeights.Bold;
            fpsDisplay.Foreground = Brushes.Chartreuse;
            fpsDisplay.Visibility = Visibility.Hidden;
            fpsDisplay.ContentStringFormat = "FPS: {0:F0}";
            MiniGameUiCanvas.Children.Add(fpsDisplay);
        };

        KeyDown += (_, e) =>
        {
            //toggable debug fps
            if (e.Key == Key.Tab)
            {
                var vis = fpsDisplay.Visibility switch
                {
                    Visibility.Visible => Visibility.Hidden,
                    Visibility.Hidden => Visibility.Visible,
                    _ => Visibility.Visible
                };

                fpsDisplay.Visibility = vis;
            }
        };


        PetAttributeHelper.InitAttributes(brain, out var collectedFood, out var collectedThirst);
        CollectedFoodAttribute = collectedFood;
        CollectedThirstAttribute = collectedThirst;

        DataContext = this;
    }

    public abstract string GameName { get; }
    protected abstract Canvas MiniGameUiCanvas { get; }

    public PetAttribute CollectedFoodAttribute { get; }
    public PetAttribute CollectedThirstAttribute { get; }

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
        _brain.PetWindow.KeyDown -= TogglePauseMenu;
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
        if (fpsDisplay == null) return;

        fpsDisplay.Content = 1000f / delta;
    }

    private void ShowPauseMenu()
    {
        StopTicking();
        _brain.PetWindow.StopTicking();
        _isPaused = true;

        if (_pauseMenu == null)
        {
            _pauseMenu = (PauseMenu)FindResource("PauseMenuControl");
            _pauseMenu.ResumeClicked += HidePauseMenu;
            _pauseMenu.LeaveClicked += () =>
            {
                _brain.PetWindow.StartTicking();
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
        _brain.PetWindow.StartTicking();
        _isPaused = false;
    }
}