using System.Windows;
using System.Windows.Threading;
using DesktopPet.Handlers;
using DesktopPet.Interfaces;
using DesktopPet.MiniGames;

namespace DesktopPet.UI;

public partial class PetJump : TickingWindow, IMiniGame
{
    private PlatformManager _platformManager;

    private int spawnInterval;
    private Random random;

    private PetBrain _brain;

    private bool doTick = false;
    
    public PetJump(PetBrain petBrain)
    {
        InitializeComponent();
        WindowHelper.FitToScreen(this);
        
        _brain = petBrain;
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        KeyDown += (_, e) => petBrain.PetWindow.RaiseEvent(e);
        
        _platformManager = new(gameCanvas);
        _brain.PlatformManager = _platformManager;
        
        random = new Random();

        spawnInterval = random.Next(10, 50);
    }

    protected override void Tick()
    {
        _platformManager.Tick();

        spawnInterval--;

        if (spawnInterval == 0)
        {
            var platformWidth = 150;
            _platformManager.SpawnPlatform(
                random.NextDouble() * (SystemParameters.FullPrimaryScreenWidth - platformWidth),
                platformWidth, 30,
                1);
            spawnInterval = random.Next(50, 120);
        }
    }

    public void Start()
    {
        doTick = true;
    }

    public void End()
    {
        doTick = false;
        //Todo Game Result
        Close();
    }
}