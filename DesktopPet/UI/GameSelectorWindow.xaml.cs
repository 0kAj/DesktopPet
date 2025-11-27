using System.Windows;
using DesktopPet.Data.Attributes;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;
using DesktopPet.Utils;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.UI;

public partial class GameSelectorWindow : Window
{
    private readonly PetBrain _brain;

    public GameSelectorWindow(PetBrain petBrain)
    {
        InitializeComponent(); //todo improve the game-selector visually
        _brain = petBrain;
        Title += " - " + _brain.Name;

        PetNameTb.Text = _brain.Name;

        PetAttributeHelper.InitAttributes(_brain, out var hunger, out var thirst, out var collectedFood,
            out var collectedThirst);
        PetHungerAttribute = hunger;
        PetThirstAttribute = thirst;
        CollectedFoodAttribute = collectedFood;
        CollectedThirstAttribute = collectedThirst;

        DataContext = this;

        SizingHelper.FitToScreen(this);
    }

    public PetAttribute PetHungerAttribute { get; }
    public PetAttribute PetThirstAttribute { get; }
    public PetAttribute CollectedFoodAttribute { get; }
    public PetAttribute CollectedThirstAttribute { get; }

    private void Feed_button_OnClick(object sender, RoutedEventArgs e)
    {
        //start FeedGame
        GameManager.Instance.StartGame("Food Collector", _brain);
        Close();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void FoodCollector_Click(object sender, RoutedEventArgs e)
    {
        GameManager.Instance.StartGame("Food Collector", _brain);
        Close();
    }

    private void PetJump_Click(object sender, RoutedEventArgs e)
    {
        GameManager.Instance.StartGame("Pet Jump", _brain);
        Close();
    }
}