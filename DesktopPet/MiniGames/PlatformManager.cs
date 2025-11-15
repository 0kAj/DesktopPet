using System.Windows;
using System.Windows.Controls;
using DesktopPet.MiniGames.GameObjects;

namespace DesktopPet.MiniGames;

public class PlatformManager
{
    private readonly Canvas _canvas;
    public List<FallingPlatform> Platforms { get; } = new();

    public PlatformManager(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void Tick()
    {
        foreach (var p in Platforms)
        {
            p.Tick();
            Canvas.SetLeft(p.Visual, p.Rect.X);
            Canvas.SetTop(p.Visual, p.Rect.Y);
            p.Visual.Visibility = Visibility.Visible;
        }

        // remove platforms outside of screen
        for (int i = Platforms.Count - 1; i >= 0; i--)
        {
            if (Platforms[i].Rect.Top > SystemParameters.WorkArea.Bottom)
            {
                _canvas.Children.Remove(Platforms[i].Visual);
                Platforms.RemoveAt(i);
            }
        }
    }

    public void SpawnPlatform(double x, double width, double height, double velocityY)
    {
        var p = new FallingPlatform(x, -height, width, height, velocityY);
        Platforms.Add(p);
        p.Visual.Visibility = Visibility.Hidden;
        
        _canvas.Children.Add(p.Visual);
    }
}
