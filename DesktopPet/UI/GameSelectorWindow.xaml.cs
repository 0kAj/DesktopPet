using System.Windows;

namespace DesktopPet.UI;

public partial class GameSelectorWindow : Window
{
    private PetWindow _petWindow;
    public GameSelectorWindow(PetWindow petWindow)
    {
        InitializeComponent();
        _petWindow = petWindow;
    }

    private void Feed_button_OnClick(object sender, RoutedEventArgs e)
    {
        //start FeedGameWindow
        StartGame(GameType.FoodCollector, _petWindow);
        Close();
    }
    
    public enum GameType
    {
        FoodCollector
    }

    public static void StartGame(GameType gameType, PetWindow petWindow)
    {
        switch (gameType)
        {
            case GameType.FoodCollector:
                FoodCollectorMiniGameWindow foodCollectorMiniGameWindow = new FoodCollectorMiniGameWindow(petWindow);
                foodCollectorMiniGameWindow.Show();
                foodCollectorMiniGameWindow.Start();
                break;
            default:
                MessageBox.Show("Could not find Gametype: " + gameType);
                break;
        }
        
    }
}