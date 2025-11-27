using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.UI;

public partial class GameSelectorWindow : Window, INotifyPropertyChanged
{
    private readonly PetBrain _brain;
    
    private const string CollectedThirstAttributeName = "collectedThirst";
    private const string CollectedFoodAttributeName = "collectedFood";
    
    public PetAttribute FoodAttribute { get; }
    public PetAttribute ThirstAttribute { get; }

    public GameSelectorWindow(PetBrain petBrain)
    {
        InitializeComponent(); //todo improve the game-selector visually
        _brain = petBrain;
        Title += " - " + _brain.Name;

        //todo bars with bindings LATER
        hungerbar.Value = Convert.ToDouble(PetManager.Instance.GetAttribute(_brain.Name, "hunger"));
        thirstbar.Value = Convert.ToDouble(PetManager.Instance.GetAttribute(_brain.Name, "thurst"));
        petName_tb.Text = _brain.Name;
        
        var pet = PetManager.Instance.GetPet(_brain.Name);
        
        FoodAttribute = pet.Attributes.FirstOrDefault(attr => attr.Name == CollectedFoodAttributeName) 
                        ?? new PetAttribute(CollectedFoodAttributeName, "0"); //default value
        
        ThirstAttribute = pet.Attributes.FirstOrDefault(attr => attr.Name == CollectedThirstAttributeName)
                          ?? new PetAttribute(CollectedThirstAttributeName, "0");

        // save defaults if required
        PetManager.Instance.SetAttribute(_brain.Name, FoodAttribute);
        PetManager.Instance.SetAttribute(_brain.Name, ThirstAttribute);
        
        DataContext = this;
    }

    private void Feed_button_OnClick(object sender, RoutedEventArgs e)
    {
        //start FeedGame
        GameManager.Instance.StartGame("Food Collector", _brain);
        Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}