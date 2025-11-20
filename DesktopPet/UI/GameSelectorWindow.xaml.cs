using System.Windows;
using DesktopPet.Data.Pet;
using DesktopPet.Handlers;

namespace DesktopPet.UI;

public partial class GameSelectorWindow : Window
{
    public enum GameType //todo this enum should be external
    {
        FoodCollector,
        PetJump
    }

    private readonly PetBrain _brain;

    public GameSelectorWindow(PetBrain petBrain)
    {
        InitializeComponent(); //todo improve the game-selector visually
        _brain = petBrain;
        Title += " - " + _brain.Name;

        hungerbar.Value = Convert.ToDouble(PetManager.Instance.GetAttribute(_brain.Name, "hunger"));
        thirstbar.Value = Convert.ToDouble(PetManager.Instance.GetAttribute(_brain.Name, "thurst"));
        petName_tb.Text = _brain.Name;
    }

    private void Feed_button_OnClick(object sender, RoutedEventArgs e)
    {
        //start FeedGameWindow
        StartGame(GameType.FoodCollector, _brain);
        Close();
    }

    public static void StartGame(GameType gameType, PetBrain petBrain) //todo this method should be external
    {
        switch (gameType)
        {
            case GameType.FoodCollector:
                var foodCollectorMiniGameWindow = new FoodCollectorMiniGameWindow(petBrain);
                foodCollectorMiniGameWindow.Show();
                foodCollectorMiniGameWindow.Start();
                break;
            case GameType.PetJump:
                var petJump = new PetJump(petBrain);
                petJump.Show();
                petJump.Start();
                break;
            default:
                MessageBox.Show("Could not find Gametype: " + gameType);
                break;
        }
    }
}