using System.Collections.ObjectModel;
using System.Windows;
using DesktopPet.MiniGames.GameObjects.Platforms;

namespace DesktopPet.MiniGames;

public class PlatformManager
{
    private readonly int _maxTicksForSpeedMultiplier;
    private readonly Random _random;
    private readonly int _speedMultiplier;

    private int _tickCounter;

    public PlatformManager()
    {
        _random = new Random();
        _speedMultiplier = 1;
        _maxTicksForSpeedMultiplier = 0;
    }

    public PlatformManager(int maxTicksForSpeedMultiplier, int speedMultiplier)
    {
        _random = new Random();
        _maxTicksForSpeedMultiplier = maxTicksForSpeedMultiplier;
        _speedMultiplier = speedMultiplier;
    }

    public ObservableCollection<FallingPlatform> Platforms { get; } = new();

    public void Tick()
    {
        _tickCounter++;

        for (var i = 0; i < Platforms.Count; i++)
        {
            var p = Platforms[i];

            if (_tickCounter < _maxTicksForSpeedMultiplier)
                // _speedMultiplier * velocity for first _maxTicksForSpeedMultiplier ticks
                p.CurrentVelocityY = p.DefaultVelocityY * _speedMultiplier;
            else if (_tickCounter == _maxTicksForSpeedMultiplier)
                // after reset to their default velocityY
                p.CurrentVelocityY = p.DefaultVelocityY;


            // Tick
            p.Tick();

            // Offscreen?
            if (p.Y > SystemParameters.WorkArea.Bottom) Remove(i);
        }
    }

    private void Remove(int i)
    {
        Platforms.RemoveAt(i);
    }

    public void SpawnRandomPlatform(double x, double width, double height, double velocityY)
    {
        switch (_random.Next(4))
        {
            case 0:
                SpawnPlatform(x, width, height, velocityY); break;
            case 1: SpawnOneShotPlatform(x, width, height, velocityY); break;
            case 2: SpawnStretchingPlatform(x, width, height, velocityY); break;
            case 3: SpawnJumpPlatform(x, width, height, velocityY); break;
        }
    }

    private void SpawnPlatform(double x, double width, double height, double velocityY)
    {
        var p = new FallingPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    private void SpawnOneShotPlatform(double x, double width, double height, double velocityY)
    {
        var p = new OneShotPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    private void SpawnStretchingPlatform(double x, double width, double height, double velocityY)
    {
        var p = new StretchingPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    private void SpawnJumpPlatform(double x, double width, double height, double velocityY)
    {
        var p = new JumpPlatform(x, -height, width, height, velocityY);
        AddPlatformToCanvas(p);
    }

    private void AddPlatformToCanvas(FallingPlatform p)
    {
        Platforms.Add(p);
    }
}