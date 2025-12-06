using System.Windows;
using DesktopPet.WPF.WindowViewModels;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.WPF;

public partial class RewardsWindow : Window
{
    public RewardsWindow(int foodScore, int thirstScore)
    {
        InitializeComponent();

        var vm = new RewardsWindowViewModel(foodScore, thirstScore);
        DataContext = vm;
        vm.RequestClose += Close;
    }
}