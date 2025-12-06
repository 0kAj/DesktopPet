using System.Windows.Media;

namespace DesktopPet.MiniGames.GameObjects.Platforms;

public class StretchingPlatform : FallingPlatform
{
    private const double StretchAmount = 1;
    private readonly double _initialWidth;
    private int _stretchDirection = 1;

    public StretchingPlatform(double x, double y, double width, double height, double velocityY) : base(x, y, width,
        height, velocityY)
    {
        _initialWidth = width;
    }

    public StretchingPlatform(double x, double y, double width, double height, double velocityY, Brush color) : base(x,
        y, width, height, velocityY, color)
    {
        _initialWidth = width;
    }

    public override void Tick()
    {
        base.Tick();

        var oldWidth = PlatformWidth;
        PlatformWidth += StretchAmount * _stretchDirection;

        // max 2x initWidth || min initWidth/2
        if (PlatformWidth > 2 * _initialWidth || PlatformWidth < _initialWidth / 2)
            _stretchDirection *= -1;

        // fix center
        X -= (PlatformWidth - oldWidth) / 2;
    }
}