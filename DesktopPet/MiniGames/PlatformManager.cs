using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DesktopPet.MiniGames.GameObjects;
using DesktopPet.MiniGames.GameObjects.Platforms;
using DesktopPet.WPF.GameWindows.customControls.gameobjects;

namespace DesktopPet.MiniGames;

public class PlatformManager
{
    private readonly Canvas _canvas;

    public PlatformManager(Canvas canvas)
    {
        _canvas = canvas;
    }

    public List<FallingPlatform> Platforms { get; } = new();

    public void Tick()
    {
        foreach (var p in Platforms)
        {
            p.Tick();
        }

        // remove platforms outside of screen
        for (var i = 0; i < Platforms.Count; i++)
        {
            var fallingPlatform = Platforms[i];
            if (fallingPlatform.GetCollisionRect().Top > SystemParameters.WorkArea.Bottom)
            {
                _canvas.Children.Remove(fallingPlatform.View);
                Platforms.RemoveAt(i);
            }
        }
    }

    public void SpawnPlatform(double x, double width, double height, double velocityY)
    {
        var p = new FallingPlatform(x, -height, width, height, velocityY);
        Platforms.Add(p);

        var view = new PlatformView()
        {
            DataContext = p,
            Width = width,
            Height = height
        };
        
        // Position via Binding
        view.SetBinding(Canvas.LeftProperty, new Binding("X"));
        view.SetBinding(Canvas.TopProperty, new Binding("Y"));
        
        _canvas.Children.Add(view);
    }
}