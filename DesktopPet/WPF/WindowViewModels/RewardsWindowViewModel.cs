using CommunityToolkit.Mvvm.Input;

namespace DesktopPet.WPF.WindowViewModels;

public partial class RewardsWindowViewModel
{
    public RewardsWindowViewModel(int foodScore, int thirstScore)
    {
        FoodScore = foodScore;
        ThirstScore = thirstScore;
    }

    public int FoodScore { get; }
    public int ThirstScore { get; }
    public event Action? RequestClose;

    [RelayCommand]
    private void Close()
    {
        RequestClose?.Invoke();
    }
}