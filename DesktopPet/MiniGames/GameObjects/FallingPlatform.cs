using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesktopPet.MiniGames.GameObjects;

public class FallingPlatform
{
    public Rect Rect { get; private set; }
    public double VelocityY { get; private set; }
    public Rectangle Visual { get; }

    public FallingPlatform(double x, double y, double width, double height, double velocityY)
    {
        Rect = new Rect(x, y, width, height);
        VelocityY = velocityY;

        Visual = new Rectangle
        {
            Width = width,
            Height = height,
            Fill = Brushes.DarkGreen
        };
    }

    public void Tick()
    {
        Rect = new Rect(Rect.X, Rect.Y + VelocityY, Rect.Width, Rect.Height);
    }

    public Rect GetCollisionRect() => Rect;
}

