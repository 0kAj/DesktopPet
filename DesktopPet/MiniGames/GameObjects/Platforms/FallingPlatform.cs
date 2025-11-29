using System.Windows;
using System.Windows.Media;
using DesktopPet.Handlers;
using DesktopPet.WPF.GameWindows.customControls.gameobjects;

namespace DesktopPet.MiniGames.GameObjects.Platforms;

public class FallingPlatform : GameObject
{
    private Brush _color;
    public Brush Color
    {
        get => _color;
        set { _color = value; OnPropertyChanged(); }
    }

    private double _platformWidth;
    public double PlatformWidth
    {
        get => _platformWidth;
        protected set { _platformWidth = value; OnPropertyChanged(); }
    }

    private double _platformHeight;
    public double PlatformHeight
    {
        get => _platformHeight;
        protected set { _platformHeight = value; OnPropertyChanged(); }
    }
    
    public double DefaultVelocityY { get; protected set; }
    public double CurrentVelocityY { get; set; } 
    
    public PlatformView View { get; set; }
    
    public FallingPlatform(double x, double y, double width, double height, double velocityY)
    : this(x, y, width, height, velocityY, Brushes.DarkGreen)
    {
    }
    
    public FallingPlatform(double x, double y, double width, double height, double velocityY, Brush color)
    {
        X = x;
        Y = y;
        PlatformWidth = width;
        PlatformHeight = height;
        DefaultVelocityY = velocityY;
        CurrentVelocityY = velocityY;
        _color = color;
    }
    
    public virtual void Tick()
    {
        Y += CurrentVelocityY;
    }

    public Rect GetCollisionRect()
    {
        return new Rect(X, Y, PlatformWidth, PlatformHeight);
    }
    
    public virtual void OnPlayerContact(PetBrain player) { }

}