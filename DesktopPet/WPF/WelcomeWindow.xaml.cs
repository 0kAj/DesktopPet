using System.Windows;
using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.WPF;

public partial class WelcomeWindow : Window
{
    public WelcomeWindow()
    {
        var defaultPet = PetManager.Instance.GetDefaultPet();
        if (defaultPet != null)
        {
            ShowPetWindow(defaultPet.PetName);
            Close();
        }

        InitializeComponent(); // only show if no default pet set
    }

    private PetWindow ShowPetWindow(string petName)
    {
        var petWindow = new PetWindow(petName);
        petWindow.Show();
        return petWindow;
    }

    private void OKButton_OnClick(object sender, RoutedEventArgs e)
    {
        // create new Pet
        var petName = NameTb.Text;
        if (string.IsNullOrWhiteSpace(petName))
        {
            ErrorTb.Text = "Pet name cannot be empty.";
            ErrorTb.Visibility = Visibility.Visible;
            return;
        }

        PetManager.Instance.SetAttribute(petName, new PetAttribute("thurst", "100"));
        PetManager.Instance.SetAttribute(petName, new PetAttribute("hunger", "100"));
        PetManager.Instance.SetDefaultPet(petName);

        if (SetAsDefaultPetCb.IsChecked == true)
            PetManager.Instance.SetDefaultPet(petName);

        ShowPetWindow(petName);

        Close();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}