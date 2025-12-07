using System.Windows;
using System.Windows.Controls;
using DesktopPet.Attribute;
using DesktopPet.Engine;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;

namespace DesktopPet.WPF.GameWindows;

[MiniGame("Pet Jump")]
public partial class PetJumpMiniGameWindow : MiniGameWindow
{
    private const int MaxTicksForSpeedMultiplier = 200;
    private const int SpeedMultiplier = 5;
    private readonly PlatformManager _platformManager;
    private readonly Random random;

    private int _foodScore;
    private int _thirstScore;
    private int _tickCounter;

    private int spawnInterval;

    public PetJumpMiniGameWindow(PetBrain petBrain) : base(petBrain) //todo I NEED VIEWMODEL
    {
        InitializeComponent();
        SizingHelper.FitToScreen(this);

        Brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        KeyDown += (_, e) => petBrain.PetWindow.RaiseEvent(e);

        _platformManager = new PlatformManager(GameCanvas, MaxTicksForSpeedMultiplier, SpeedMultiplier);
        Brain.PlatformManager = _platformManager;

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

    protected override void End()
    {
        base.End();
        CollectedFood += _foodScore;
        StopTicking();
        Brain.PlatformManager = null;
        Brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);
        var rewardsWindow = new RewardsWindow(_foodScore, _thirstScore);
        rewardsWindow.Show();
        Close();
    }

    protected override void Tick()
    {
        _platformManager.Tick();

        _tickCounter++;
        spawnInterval--;

        // SpeedMultiplier * velocity for first MaxTicksForSpeedMultiplier ticks
        var speedFactor = _tickCounter <= MaxTicksForSpeedMultiplier ? 1.0 / SpeedMultiplier : 1.0;

        if (spawnInterval == 0)
        {
            // todo make special platforms that spawn with special items like coins/hunger or bottles

            var platformWidth = 150;
            _platformManager
                .SpawnRandomPlatform(
                    random.NextDouble() * (SystemParameters.FullPrimaryScreenWidth - platformWidth),
                    platformWidth, 30,
                    1);
            spawnInterval = (int)(random.Next(50, 120) * speedFactor);
        }
    }

    private void UpdateCollectableDisplay()
    {
        //update score
        CDisplay.ThirstTb.Text = _thirstScore.ToString();

        CDisplay.FoodTb.Text = _foodScore.ToString();
    }
}