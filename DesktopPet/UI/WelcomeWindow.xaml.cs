using System.Windows;

namespace DesktopPet.UI;

public partial class WelcomeWindow : Window
{
    public WelcomeWindow()
    {
        InitializeComponent();
        //todo Check for stored pets
        //todo load default stored pet
    }

    private void OKButton_OnClick(object sender, RoutedEventArgs e)
    {
        //todo create new Pet
        PetWindow petWindow = new PetWindow();
        petWindow.Show();
        Close();
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}