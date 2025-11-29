using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DesktopPet.MiniGames.GameObjects.Platforms;
using DesktopPet.WPF.GameWindows.customControls.gameobjects;

namespace DesktopPet.MiniGames;

public class PlatformManager
{
    private readonly Canvas _canvas;
    private readonly Random _random;
    
    private int _tickCounter;
    private int _maxTicksForSpeedMultiplier;
    private int _speedMultiplier;

    public PlatformManager(Canvas canvas)
    {
        _canvas = canvas;
        _random = new Random();
        _speedMultiplier = 1;
        _maxTicksForSpeedMultiplier = 0;
    }

    public PlatformManager(Canvas canvas, int maxTicksForSpeedMultiplier, int speedMultiplier)
    {
        _canvas = canvas;
        _random = new Random();
        _maxTicksForSpeedMultiplier = maxTicksForSpeedMultiplier;
        _speedMultiplier = speedMultiplier;
    }

    public List<FallingPlatform> Platforms { get; } = new();

    public void Tick()
    {
        _tickCounter++;
        
        for (int i = 0; i < Platforms.Count; i++)
        {
            var p = Platforms[i];
            
            if (_tickCounter <= _maxTicksForSpeedMultiplier)
            {
                // _speedMultiplier * velocity for first _maxTicksForSpeedMultiplier ticks
                p.CurrentVelocityY = p.DefaultVelocityY * _speedMultiplier;
            }
            else
            {
                // after reset to their default velocityY
                // p.CurrentVelocityY = p.DefaultVelocityY;
                p.CurrentVelocityY = 0;
            }
            

            // Tick
            p.Tick();

            // Offscreen?
            if (p.Y > SystemParameters.WorkArea.Bottom)
            {
                Remove(i);
            }
        }
    }

    void Remove(int i)
    {
        _canvas.Children.Remove(Platforms[i].View);
        Platforms.RemoveAt(i);
    }

    public void SpawnRandomPlatform(double x, double width, double height, double velocityY)
    {
        switch (_random.Next(4)) //todo create Platform-registry
        {
            case 0: SpawnPlatform(x, width, height, velocityY); break;
            case 1: SpawnOneShotPlatform(x, width, height, velocityY); break;
            case 2: SpawnStretchingPlatform(x, width, height, velocityY); break;
            case 3: SpawnJumpPlatform(x, width, height, velocityY); break;
        }
    }
    
    public void SpawnPlatform(double x, double width, double height, double velocityY)
    {
        var p = new FallingPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    public void SpawnOneShotPlatform(double x, double width, double height, double velocityY)
    {
        var p = new OneShotPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    public void SpawnStretchingPlatform(double x, double width, double height, double velocityY)
    {
        var p = new StretchingPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    public void SpawnJumpPlatform(double x, double width, double height, double velocityY)
    {
        var p = new JumpPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    private void AddPlatformToCanvas(FallingPlatform p)
    {
        Platforms.Add(p);

        var view = new PlatformView
        {
            DataContext = p,
        };

        view.SetBinding(Canvas.LeftProperty, new Binding("X"));
        view.SetBinding(Canvas.TopProperty, new Binding("Y"));
        
        p.View = view;
        
        _canvas.Children.Add(view);
    }
}