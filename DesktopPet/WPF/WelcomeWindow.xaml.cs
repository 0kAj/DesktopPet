using DesktopPet.WPF.WindowViewModels;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.WPF;

public partial class WelcomeWindow : Window
{
    public WelcomeWindow()
    {
        var vm = new WelcomeWindowViewModel();
        DataContext = vm;
        vm.RequestClose += Close;
        vm.RequestOpenPetWindow += ShowPetWindow;

        InitializeComponent();
    }

    private void ShowPetWindow(string petName)
    {
        new PetWindow(petName).Show();
    }
}