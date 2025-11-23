using System.Windows;
using System.Windows.Controls;
using DesktopPet.Attribute;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;

namespace DesktopPet.UI.GameWindows;

[MiniGame("Pet Jump")]
public partial class PetJumpMiniGameWindow : MiniGameWindow
{
    private readonly PlatformManager _platformManager;
    private readonly Random random;
    
    private int spawnInterval;

    private int _foodScore;
    private int _thirstScore;


    public PetJumpMiniGameWindow(PetBrain petBrain) : base(petBrain)
    {
        InitializeComponent();
        SizingHelper.FitToScreen(this);

        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        KeyDown += (_, e) => petBrain.PetWindow.RaiseEvent(e);

        _platformManager = new PlatformManager(GameCanvas);
        _brain.PlatformManager = _platformManager;

        random = new Random();

        spawnInterval = random.Next(10, 50);
        
        SetDelta(20);
        
        UpdateCollectableDisplay();
        
        TDisplay.Timer.Set(30 * 3);
        TDisplay.Timer.Timeout += () => End();
        
        TickStart += () => TDisplay.Timer.Start();
        TickStop += () => TDisplay.Timer.Stop();
    }

    public override string GameName => "Pet Jump";
    protected override Canvas MiniGameUiCanvas => UiCanvas;

    public override void Start()
    {
        StartTicking();
    }

    public override void End()
    {
        base.End();
        CollectedFood += _foodScore;
        StopTicking();
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);
        RewardsWindow rewardsWindow = new RewardsWindow(_foodScore, _thirstScore);
        rewardsWindow.Show();
        Close();
    }

    protected override void Tick()
    {
        _platformManager.Tick();

        spawnInterval--;

        if (spawnInterval == 0)
        {
            // todo make special platforms that spawn with special items like coins/hunger or bottles:
            // platform type for red == food; blue == thirst; yellow == high jump;
            // on collision change platform color to gray
            var platformWidth = 150;
            _platformManager
                .SpawnPlatform(
                    random.NextDouble() * (SystemParameters.FullPrimaryScreenWidth - platformWidth),
                    platformWidth, 30,
                    1);
            spawnInterval = random.Next(50, 120);
        }
    }
    
    private void UpdateCollectableDisplay()
    {
        //update score
        CDisplay.Thirst_tb.Text = _thirstScore.ToString();
        
        CDisplay.Food_tb.Text = _foodScore.ToString();
    }
}