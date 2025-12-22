using DesktopPet.WPF;

namespace DesktopPet.Factory;

public interface IRewardsWindowFactory
{
    RewardsWindow Create(int foodScore, int thirstScore);
}