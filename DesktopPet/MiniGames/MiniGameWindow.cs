using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopPet.Data.Pet;
using DesktopPet.Handlers;
using DesktopPet.UI.GameWindows.customControls;

namespace DesktopPet.MiniGames;

public abstract class MiniGameWindow : TickingWindow
{
    protected readonly PetBrain _brain;

    protected MiniGameWindow(PetBrain brain)
    {
        _brain = brain;
        brain.PetWindow.KeyDown += TogglePauseMenu;
    }

    public abstract string GameName { get; }
    protected abstract Canvas MiniGameUiCanvas { get; }

    public abstract void Start();

    public virtual void End()
    {
        _brain.PetWindow.KeyDown -= TogglePauseMenu;
    }
    
    private PauseMenu _pauseMenu;
    private bool _isPaused;

    private void TogglePauseMenu(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Escape) return;
            
        if (_isPaused)
            HidePauseMenu();
        else
            ShowPauseMenu();
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
            _pauseMenu.QuitClicked += () => Application.Current.Shutdown();
            MiniGameUiCanvas.Children.Add(_pauseMenu);
            
            SizingHelper.FitToScreen(_pauseMenu);
            
            Canvas.SetLeft(_pauseMenu, (MiniGameUiCanvas.ActualWidth - _pauseMenu.Width)/2);
            Canvas.SetTop(_pauseMenu, (MiniGameUiCanvas.ActualHeight - _pauseMenu.Height)/2);
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
    
    private const string CollectedThirstAttributeName = "collectedThirst";
    private const string CollectedFoodAttributeName = "collectedFood";

    public int CollectedThirst
    {
        get => Convert.ToInt32(PetManager.Instance.GetAttribute(_brain.Name, CollectedThirstAttributeName, "0"));
        set => PetManager.Instance.SetAttribute(_brain.Name, CollectedThirstAttributeName, value.ToString());
    } 
    public int CollectedFood
    {
        get => Convert.ToInt32(PetManager.Instance.GetAttribute(_brain.Name, CollectedFoodAttributeName, "0"));
        set => PetManager.Instance.SetAttribute(_brain.Name, CollectedFoodAttributeName, value.ToString());
    }
}