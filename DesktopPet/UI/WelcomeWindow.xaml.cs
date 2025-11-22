using System.Windows;
using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;

namespace DesktopPet.UI;

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
        var petName = name_tb.Text;
        if (string.IsNullOrWhiteSpace(petName))
        {
            error_tb.Text = "Pet name cannot be empty.";
            error_tb.Visibility = Visibility.Visible;
            return;
        }

        PetManager.Instance.SetAttribute(petName, new PetAttribute("thurst", "100"));
        PetManager.Instance.SetAttribute(petName, new PetAttribute("hunger", "100"));
        PetManager.Instance.SetDefaultPet(petName);

        if (setAsDefaultPet_cb.IsChecked == true)
            PetManager.Instance.SetDefaultPet(petName);

        ShowPetWindow(petName);

        Close();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}