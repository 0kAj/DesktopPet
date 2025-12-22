using CommunityToolkit.Mvvm.Input;
using DesktopPet.MiniGames;

namespace DesktopPet.WPF.WindowViewModels;

public partial class RewardsWindowViewModel
{
    public RewardsWindowViewModel(RewardsData data)
    {
        FoodScore = data.FoodScore;
        ThirstScore = data.ThirstScore;
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