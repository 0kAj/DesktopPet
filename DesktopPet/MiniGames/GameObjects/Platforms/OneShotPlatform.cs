using System.Windows.Media;
using DesktopPet.Handlers;

namespace DesktopPet.MiniGames.GameObjects.Platforms;

public class OneShotPlatform : FallingPlatform
{
    private bool _used = false;

    public OneShotPlatform(double x, double y, double width, double height, double velocityY) : 
        this(x, y, width, height, velocityY, Colors.DarkGreen)
    {
    }

    public OneShotPlatform(double x, double y, double width, double height, double velocityY, Color color) :
        base(x, y, width, height, velocityY, new SolidColorBrush(color) { Opacity = 0.5 })
    {
    }

    public override void OnPlayerContact(PetBrain player)
    {
        base.OnPlayerContact(player);
        if (_used) return;

        _used = true;
        CurrentVelocityY = 8; // falldown
    }
}