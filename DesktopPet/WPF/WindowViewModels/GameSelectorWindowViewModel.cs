using ColorPicker;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopPet.Data.Attributes;
using DesktopPet.Handlers;
using DesktopPet.Handlers.Events;
using DesktopPet.MiniGames;
using DesktopPet.Utils;

namespace DesktopPet.WPF.WindowViewModels;

public partial class GameSelectorWindowViewModel : ObservableObject
{
    private readonly PetBrain _brain;

    public GameSelectorWindowViewModel(PetBrain brain)
    {
        _brain = brain;

        PetAttributeHelper.InitAttributes(_brain.Name, out var hunger, out var thirst, out var collectedFood,
            out var collectedThirst);
        PetHungerAttribute = hunger;
        PetThirstAttribute = thirst;
        CollectedFoodAttribute = collectedFood;
        CollectedThirstAttribute = collectedThirst;
        
        PetAttributeHelper.InitPetColorAttributes(_brain.Name, out var primaryColor, out var secondaryColor);
        PrimaryColorAttribute = primaryColor;
        SecondaryColorAttribute = secondaryColor;
    }

    public PetAttribute PetHungerAttribute { get; }
    public PetAttribute PetThirstAttribute { get; }
    public PetAttribute CollectedFoodAttribute { get; }
    public PetAttribute CollectedThirstAttribute { get; }
    public PetAttribute PrimaryColorAttribute { get; set; }
    public PetAttribute SecondaryColorAttribute { get; set; }

    [ObservableProperty]
    private bool showColorPickers = false;

    public string PetName => _brain.Name;
    public string Title => "Game Selector - " + _brain.Name;

    public event Action? RequestClose;

    [RelayCommand]
    private void Feed()
    {
        var petHunger = int.TryParse(PetHungerAttribute.Value, out var p) ? p : 0;
        if (petHunger == 100) return;
        var collectedFood = int.TryParse(CollectedFoodAttribute.Value, out var c) ? c : 0;
        
        // cap 100 step-max. 5
        var maxAmount = Math.Min(100 - petHunger, collectedFood);
        var amount = Math.Min(maxAmount, 5);
        
        PetHungerAttribute.Value = (petHunger + amount).ToString();
        CollectedFoodAttribute.Value = (collectedFood - amount).ToString();
    }

    [RelayCommand]
    private void Thirst()
    {
        var petThirst = int.TryParse(PetThirstAttribute.Value, out var p) ? p : 0;
        if (petThirst == 100) return;
        var collectedThirst = int.TryParse(CollectedThirstAttribute.Value, out var c) ? c : 0;

        // cap 100 step-max. 5
        var maxAmount = Math.Min(100 - petThirst, collectedThirst);
        var amount = Math.Min(maxAmount, 5);
        
        PetThirstAttribute.Value = (petThirst + amount).ToString();
        CollectedThirstAttribute.Value = (collectedThirst - amount).ToString();
    }

    [RelayCommand]
    private void Close()
    {
        RequestClose?.Invoke();
    }

    [RelayCommand]
    private void FoodCollector()
    {
        GameManager.Instance.StartGame("Food Collector", _brain);
        Close();
    }

    [RelayCommand]
    private void PetJump()
    {
        GameManager.Instance.StartGame("Pet Jump", _brain);
        Close();
    }

    [RelayCommand]
    private void ColorPicker()
    {
        ShowColorPickers = !ShowColorPickers;
    }
}