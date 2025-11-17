using System.Windows;

namespace DesktopPet;

public class VelocityWindow : TickingWindow
{
    public double VelocityX { get; set; }
    public double VelocityY { get; set; }

    protected override void Tick()
    {
        Top += VelocityY;
        Left += VelocityX;

        var screenWidth = SystemParameters.PrimaryScreenWidth;
        var screenHeight = SystemParameters.PrimaryScreenHeight;

        var rect = GetCollisionRect();

        //todo work with clamp
        // X-Collision
        if (rect.Left < 0)
        {
            Left -= rect.Left; // rect.Left = 0
            VelocityX = 0;
        }
        else if (rect.Right > screenWidth)
        {
            Left -= rect.Right - screenWidth;
            VelocityX = 0;
        }

        // Y-Collision
        if (rect.Top < 0)
        {
            Top -= rect.Top;
            VelocityY = 0;
        }
        else if (rect.Bottom > screenHeight)
        {
            Top -= rect.Bottom - screenHeight;
            VelocityY = 0;
        }
    }

    public virtual Rect GetCollisionRect()
    {
        return new Rect(Left, Top, Width, Height);
    }

    public void ResetVelocity()
    {
        VelocityX = 0;
        VelocityY = 0;
    }
}