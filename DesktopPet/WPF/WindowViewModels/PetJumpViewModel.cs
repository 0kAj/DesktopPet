using System.Collections.ObjectModel;
using System.Windows;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;
using DesktopPet.MiniGames.GameObjects.Platforms;

namespace DesktopPet.WPF.WindowViewModels;

public class PetJumpViewModel : MiniGameViewModel
{
    private readonly PlatformManager _platformManager;
    
    private int _spawnInterval;
    
    public PetJumpViewModel(PetBrain brain) : base(brain)
    {
        _platformManager = new PlatformManager(MaxTicksForSpeedMultiplier, SpeedMultiplier);
        brain.PlatformManager = _platformManager;
        
        _spawnInterval = Rand.Next(10, 50);
    }
    
    public ObservableCollection<FallingPlatform> Platforms => _platformManager.Platforms;
    
    public override void Tick()
    {
        base.Tick();
        _platformManager.Tick();

        _spawnInterval--;
        
        if (_spawnInterval == 0)
        {
            var platformWidth = 150;

            var posX = Rand.NextDouble() * (SystemParameters.FullPrimaryScreenWidth - platformWidth);
            var velocityY = 1;

            // add collectable on Platforms at x/4 x/2 and x3/4 - collectable Width /2
            var positions = new[]
            {
                posX + platformWidth / 4.0 - 10,
                posX + platformWidth / 2.0 - 10,
                posX + platformWidth * 3.0 / 4 - 10
            };

            foreach (var pX in positions)
            {
                switch (Rand.Next(4))
                {
                    case 0:
                        CreateFood(pX, -80, velocityY);
                        break;
                    case 1:
                        CreateThirst(pX, -80, velocityY);
                        break;
                    case 2:
                    case 3:
                        break;
                }
            }
            
            _platformManager
                .SpawnRandomPlatform(
                    posX,
                    platformWidth, 30,
                    velocityY);
            _spawnInterval = (int)(Rand.Next(50, 120) * SpeedFactor);
        }
    }
}