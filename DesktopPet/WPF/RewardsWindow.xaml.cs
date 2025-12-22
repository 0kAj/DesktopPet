using DesktopPet.WPF.WindowViewModels;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.WPF;

public partial class RewardsWindow : Window
{
    public RewardsWindow(RewardsWindowViewModel vm)
    {
        InitializeComponent();

        DataContext = vm;
        vm.RequestClose += Close;
    }
}