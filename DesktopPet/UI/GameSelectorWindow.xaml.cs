using System.Windows;
using DesktopPet.Handlers;

namespace DesktopPet.UI;

public partial class GameSelectorWindow : Window
{
    private PetBrain _brain;
    public GameSelectorWindow(PetBrain petBrain)
    {
        InitializeComponent();
        _brain = petBrain;
    }

    private void Feed_button_OnClick(object sender, RoutedEventArgs e)
    {
        //start FeedGameWindow
        StartGame(GameType.FoodCollector, _brain);
        Close();
    }
    
    public enum GameType
    {
        FoodCollector,
        PetJump
    }

    public static void StartGame(GameType gameType, PetBrain petBrain)
    {
        switch (gameType)
        {
            case GameType.FoodCollector:
                FoodCollectorMiniGameWindow foodCollectorMiniGameWindow = new FoodCollectorMiniGameWindow(petBrain);
                foodCollectorMiniGameWindow.Show();
                foodCollectorMiniGameWindow.Start();
                break;
            case GameType.PetJump:
                PetJump petJump = new PetJump(petBrain);
                petJump.Show();
                petJump.Start();
                break;
            default:
                MessageBox.Show("Could not find Gametype: " + gameType);
                break;
        }
        
    }
}