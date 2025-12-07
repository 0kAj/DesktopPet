using DesktopPet.Handlers;
using DesktopPet.WPF.WindowViewModels;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.WPF;

public partial class GameSelectorWindow : Window
{
    public GameSelectorWindow(PetBrain petBrain)
    {
        InitializeComponent();

        var vm = new GameSelectorWindowViewModel(petBrain);
        vm.RequestClose += Close;
        DataContext = vm;

        SizingHelper.FitToScreen(this);
    }
}