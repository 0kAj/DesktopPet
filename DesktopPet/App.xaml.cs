using System.Windows;
using DesktopPet.Data.Pet;
using DesktopPet.WPF;

namespace DesktopPet;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        // only show welcomewindow if no default pet set
        var defaultPet = PetManager.Instance.GetDefaultPet();
        if (defaultPet != null)
            new PetWindow(defaultPet.PetName).Show();
        else
            new WelcomeWindow().Show();
    }
}