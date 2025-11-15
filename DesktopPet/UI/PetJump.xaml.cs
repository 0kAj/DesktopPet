using System.Windows;
using DesktopPet.Handlers;
using DesktopPet.Interfaces;
using DesktopPet.MiniGames;

namespace DesktopPet.UI;

public partial class PetJump : TickingWindow, IMiniGame
{
    private readonly PetBrain _brain;
    private readonly PlatformManager _platformManager;
    private readonly Random random;

    private bool doTick;

    private int spawnInterval;

    public PetJump(PetBrain petBrain)
    {
        InitializeComponent();
        WindowHelper.FitToScreen(this);

        _brain = petBrain;
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        KeyDown += (_, e) => petBrain.PetWindow.RaiseEvent(e);

        _platformManager = new PlatformManager(gameCanvas);
        _brain.PlatformManager = _platformManager;

        random = new Random();

        spawnInterval = random.Next(10, 50);
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

    protected override void Tick()
    {
        _platformManager.Tick();

        spawnInterval--;

        if (spawnInterval == 0)
        {
            var platformWidth = 150;
            _platformManager
                .SpawnPlatform( // todo make special platforms that spawn with special items like coins/hunger or bottles
                    random.NextDouble() * (SystemParameters.FullPrimaryScreenWidth - platformWidth),
                    platformWidth, 30,
                    1);
            spawnInterval = random.Next(50, 120);
        }
    }
}