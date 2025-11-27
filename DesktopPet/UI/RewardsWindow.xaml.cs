using System.Windows;
using Window = DesktopPet.Engine.Window;

namespace DesktopPet.UI;

public partial class RewardsWindow : Window
{
    public RewardsWindow(int foodScore, int thirstScore)
    {
        InitializeComponent();

        CDisplay.Food_tb.Text = foodScore.ToString();
        CDisplay.Thirst_tb.Text = thirstScore.ToString();
        CDisplay.HorizontalAlignment = HorizontalAlignment.Center;
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }
}