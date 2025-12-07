using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DesktopPet.WPF.WindowViewModels;

public partial class FoodCollectorViewModel : ObservableObject
{
    [ObservableProperty]
    private int _foodScore;
    
    [ObservableProperty]
    private int _thirstScore;
    
    public event Action? GameFinished;
    
    [RelayCommand]
    private void FinishGame()
    {
        GameFinished?.Invoke();
    }
}