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
    
    public PetAttribute PetHungerAttribute { get; }
    public PetAttribute PetThirstAttribute { get; }
    public PetAttribute CollectedFoodAttribute { get; }
    public PetAttribute CollectedThirstAttribute { get; }

    public GameSelectorWindow(PetBrain petBrain)
    {
        InitializeComponent(); //todo improve the game-selector visually
        _brain = petBrain;
        Title += " - " + _brain.Name;
        
        petName_tb.Text = _brain.Name;
        
        PetAttributeHelper.InitAttributes(_brain, out PetAttribute hunger, out PetAttribute thirst, out PetAttribute collectedFood, out PetAttribute collectedThirst);
        PetHungerAttribute = hunger;
        PetThirstAttribute = thirst;
        CollectedFoodAttribute = collectedFood;
        CollectedThirstAttribute = collectedThirst;
        
        DataContext = this;
    }

    private void Feed_button_OnClick(object sender, RoutedEventArgs e)
    {
        //start FeedGame
        GameManager.Instance.StartGame("Food Collector", _brain);
        Close();
    }
}