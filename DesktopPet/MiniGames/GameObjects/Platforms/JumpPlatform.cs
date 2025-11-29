using System.Windows;
using System.Windows.Media;
using ColorHelper;
using DesktopPet.Handlers;

namespace DesktopPet.MiniGames.GameObjects.Platforms;

public class JumpPlatform : FallingPlatform
{
    private int _hue;
    
    private bool _isBouncing;
    private double _bounceProgress;
    private const double BounceSpeed = 0.1;
    private const double BounceAmount = 40;
    
    private double _originalHeight;
    
    private int _stretchDirection = 1;
    
    public JumpPlatform(double x, double y, double width, double height, double velocityY) : base(x, y, width, height, velocityY)
    {
        _originalHeight = PlatformHeight;
    }

    public JumpPlatform(double x, double y, double width, double height, double velocityY, Brush color) : base(x, y, width, height, velocityY, color)
    {
        _originalHeight = PlatformHeight;
    }

    public override void Tick()
    {
        base.Tick();
        
        _hue += 2;
        if (_hue > 360) _hue = 0;
        
        var rgb = ColorHelper.ColorConverter.HsvToRgb(new HSV(_hue, 100, 100)); // rainbow
        Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(rgb.R, rgb.G, rgb.B));
        
        
        // Bounce-Animation
        if (!_isBouncing) return;
        
        _bounceProgress += BounceSpeed;
        
        if (_bounceProgress > 1)
        {
            _bounceProgress = 0;
            _isBouncing = false;
            PlatformHeight = _originalHeight;
        }
        else
        {
            var bounce = BounceAmount * Math.Sin(Math.PI * _bounceProgress);
            var previousHeight = PlatformHeight;
            PlatformHeight = _originalHeight + bounce;
            
            Y -= PlatformHeight - previousHeight;
        }
    }

    public override void OnPlayerContact(PetBrain player)
    {
        base.OnPlayerContact(player);
        
        if (_isBouncing) return;
        // activate Bounce from 0
        _isBouncing = true;
        _bounceProgress = 0;
        
        player.PetWindow.VelocityY = -10; // launch player into the sky
    }
}