using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DesktopPet.Handlers;
using DesktopPet.WPF.GameWindows.customControls.gameobjects;

namespace DesktopPet.MiniGames.GameObjects.Platforms;

public partial class FallingPlatform : GameObject
{
    [ObservableProperty] private Brush _color;

    [ObservableProperty] private double _platformHeight;

    [ObservableProperty] private double _platformWidth;

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

    public double DefaultVelocityY { get; }
    public double CurrentVelocityY { get; set; }

    public PlatformView View { get; set; }

    public virtual void Tick()
    {
        Y += CurrentVelocityY;
    }

    public Rect GetCollisionRect()
    {
        return new Rect(X, Y, PlatformWidth, PlatformHeight);
    }

    public virtual void OnPlayerContact(PetBrain player)
    {
    }
}