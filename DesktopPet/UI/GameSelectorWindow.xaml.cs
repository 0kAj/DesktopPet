using System.Windows;
using DesktopPet.Data.Pet;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;

namespace DesktopPet.UI;

public partial class GameSelectorWindow : Window
{
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
        //start FeedGame
        GameManager.Instance.StartGame("Food Collector", _brain);
        Close();
    }
}