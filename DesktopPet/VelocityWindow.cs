using System.Windows;

namespace DesktopPet;

public class VelocityWindow : TickingWindow
{
    public double VelocityX { get; set;}
    public double VelocityY { get; set;}
    protected override void Tick()
    {
        Top += VelocityY;
        Left += VelocityX;

        // Bildschirmgröße holen
        double screenWidth = SystemParameters.PrimaryScreenWidth;
        double screenHeight = SystemParameters.PrimaryScreenHeight;

        double windowWidth =  Width;
        double windowHeight = Height;

        // Begrenzung in X-Richtung
        if (Left < 0)
        {
            Left = 0;
            VelocityX = 0;
        }
        else if (Left + windowWidth > screenWidth)
        {
            Left = screenWidth - windowWidth;
            VelocityX = 0;
        }

        // Begrenzung in Y-Richtung
        if (Top < 0)
        {
            Top = 0;
            VelocityY = 0;
        }
        else if (Top + windowHeight > screenHeight)
        {
            Top = screenHeight - windowHeight;
            VelocityY = 0;
        }
    }

    public void ResetVelocity()
    {
        VelocityX = 0;
        VelocityY = 0;
    }
}