using System.Windows;

namespace DesktopPet.Engine.GameObjects;

public abstract class VelocityGameObject : HighPrecisionTickingGameObject
{
    public double VelocityX { get; set; }
    public double VelocityY { get; set; }

    public Func<Rect>? GetCollisionRect { get; set; }
    public Rect CollisionRect => GetCollisionRect?.Invoke() ?? new Rect(Left, Top, WindowWidth, WindowHeight);

    public Func<Vector>? GetCollisionPositionVector { get; set; }
    public Vector CollisionPositionVector => GetCollisionPositionVector?.Invoke() ?? new Vector(Left, Top);

    protected override void Tick()
    {
        Top += VelocityY;
        Left += VelocityX;

        // var screenWidth = SystemParameters.PrimaryScreenWidth;
        var screenHeight = SystemParameters.PrimaryScreenHeight;

        var rect = CollisionRect;

        // X-Collision
        // if (rect.Left < 0)
        // {
        //     Left -= rect.Left; // rect.Left = 0
        //     VelocityX = 0;
        // }
        // else if (rect.Right > screenWidth)
        // {
        //     Left -= rect.Right - screenWidth;
        //     VelocityX = 0;
        // }

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


    public void ResetVelocity()
    {
        VelocityX = 0;
        VelocityY = 0;
    }
}