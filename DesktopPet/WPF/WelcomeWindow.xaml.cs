using DesktopPet.WPF.WindowViewModels;
using Microsoft.Extensions.DependencyInjection;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.WPF;

public partial class WelcomeWindow : Window
{
    public WelcomeWindow(WelcomeWindowViewModel vm)
    {
        DataContext = vm;
        vm.RequestClose += Close;
        vm.RequestOpenPetWindow += ShowPetWindow;

        InitializeComponent();
    }

    private void ShowPetWindow(string petName)
    {
        var eventManager = App.Host.Services.GetRequiredService<PetEventManager>();
        new PetWindow(eventManager, petName).Show();
    }
}