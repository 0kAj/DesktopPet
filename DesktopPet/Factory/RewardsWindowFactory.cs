using DesktopPet.MiniGames;
using DesktopPet.WPF;
using DesktopPet.WPF.WindowViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopPet.Factory;

public class RewardsWindowFactory : IRewardsWindowFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public RewardsWindowFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public RewardsWindow Create(int foodScore, int thirstScore)
    {
        var data = new RewardsData()
        {
            FoodScore = foodScore,
            ThirstScore = thirstScore
        };
        
        var window = ActivatorUtilities.CreateInstance<RewardsWindow>(_serviceProvider, new RewardsWindowViewModel(data));
        
        return window;
    }
}