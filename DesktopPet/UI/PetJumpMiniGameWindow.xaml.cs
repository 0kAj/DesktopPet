using System.Windows;
using DesktopPet.Attribute;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;

namespace DesktopPet.UI;

[MiniGame("Pet Jump")]
public partial class PetJumpMiniGameWindow : MiniGameWindow
{
    private readonly PlatformManager _platformManager;
    private readonly Random random;
    
    private int spawnInterval;


    public PetJumpMiniGameWindow(PetBrain petBrain) : base(petBrain)
    {
        InitializeComponent();
        WindowHelper.FitToScreen(this);

        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        KeyDown += (_, e) => petBrain.PetWindow.RaiseEvent(e);

        _platformManager = new PlatformManager(gameCanvas);
        _brain.PlatformManager = _platformManager;

        random = new Random();

        spawnInterval = random.Next(10, 50);
    }

    public override string GameName => "Pet Jump";

    public override void Start()
    {
        StartTicking();
    }

    public override void End()
    {
        StopTicking();
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