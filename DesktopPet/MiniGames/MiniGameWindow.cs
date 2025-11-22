using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopPet.Handlers;
using DesktopPet.UI.GameWindows.customControls;

namespace DesktopPet.MiniGames;

public abstract class MiniGameWindow : TickingWindow
{
    protected readonly PetBrain _brain;

    protected MiniGameWindow(PetBrain brain)
    {
        _brain = brain;
        brain.PetWindow.KeyDown += (_, e) =>
        {
            if (e.Key != Key.Escape) return;
            
            if (_isPaused)
                HidePauseMenu();
            else
                ShowPauseMenu();
        };
    }

    public abstract string GameName { get; }
    protected abstract Canvas MiniGameCanvas { get; }

    public abstract void Start();
    public abstract void End();
    
    private PauseMenu _pauseMenu;
    private bool _isPaused;

    private void ShowPauseMenu()
    {
        StopTicking();
        _brain.PetWindow.StopTicking();
        _isPaused = true;
        
        if (_pauseMenu == null)
        {
            _pauseMenu = (PauseMenu)FindResource("PauseMenuControl");
            _pauseMenu.ResumeClicked += HidePauseMenu;
            _pauseMenu.QuitClicked += () => Application.Current.Shutdown();
            MiniGameCanvas.Children.Add(_pauseMenu);
            
            SizingHelper.FitToScreen(_pauseMenu);
            
            Canvas.SetLeft(_pauseMenu, (MiniGameCanvas.ActualWidth - _pauseMenu.Width)/2);
            Canvas.SetTop(_pauseMenu, (MiniGameCanvas.ActualHeight - _pauseMenu.Height)/2);
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