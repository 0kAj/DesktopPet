using System.Windows;
using System.Windows.Media;
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

    private double Width { get; }
    private double Height { get; }
    public double VelocityY { get; }
    
    public PlatformView View { get; set; }
    
    public FallingPlatform(double x, double y, double width, double height, double velocityY)
    : this(x, y, width, height, velocityY, Brushes.DarkGreen)
    {
    }
    
    public FallingPlatform(double x, double y, double width, double height, double velocityY, Brush color)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        VelocityY = velocityY;
        _color = color;
    }
    
    public void Tick()
    {
        Y += VelocityY;
    }

    public Rect GetCollisionRect()
    {
        return new Rect(X, Y, Width, Height);
    }
}