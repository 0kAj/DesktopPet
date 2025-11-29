using System.Windows;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.WPF;

public partial class RewardsWindow : Window
{
    public RewardsWindow(int foodScore, int thirstScore)
    {
        InitializeComponent();

        CDisplay.FoodTb.Text = foodScore.ToString();
        CDisplay.ThirstTb.Text = thirstScore.ToString();
        CDisplay.HorizontalAlignment = HorizontalAlignment.Center;
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }
}