using System.Windows;
using System.Windows.Controls;
using DesktopPet.Attribute;
using DesktopPet.Engine;
using DesktopPet.Handlers;
using DesktopPet.MiniGames;
using DesktopPet.WPF.WindowViewModels;

namespace DesktopPet.WPF.GameWindows;

[MiniGame("Pet Jump")]
public partial class PetJumpMiniGameWindow : MiniGameWindow
{
    private const int MaxTicksForSpeedMultiplier = 200;
    private const int SpeedMultiplier = 5;
    private readonly PlatformManager _platformManager;
    private readonly Random _random;
    
    private int _tickCounter;

    private int _spawnInterval;
    
    private readonly MiniGameViewModel _vm;


    public PetJumpMiniGameWindow(PetBrain petBrain) : base(petBrain)
    {
        InitializeComponent();
        SizingHelper.FitToScreen(this);
        
        _vm = new MiniGameViewModel();
        DataContext = _vm;
        _vm.GameFinished += End;

        Brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        // route all key events to petwindow
        KeyDown += (_, e) => petBrain.PetWindow.RaiseEvent(e);

        _platformManager = new PlatformManager(GameCanvas, MaxTicksForSpeedMultiplier, SpeedMultiplier);
        Brain.PlatformManager = _platformManager;

        _random = new Random();

        _spawnInterval = _random.Next(10, 50);

        SetDelta(20);
        
        TDisplay.Timer.Set(30 * 3);
        TDisplay.Timer.Timeout += End;

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
        StopTicking();
        Brain.PlatformManager = null;
        // back to default AI
        Brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);
        // add Score
        CollectedFood += _vm.FoodScore;
        CollectedThirst += _vm.ThirstScore;
        // show collected score
        new RewardsWindow(_vm.FoodScore, _vm.ThirstScore).Show();
        Close();
    }

    protected override void Tick()
    {
        _platformManager.Tick();

        _tickCounter++;
        _spawnInterval--;

        // SpeedMultiplier * velocity for first MaxTicksForSpeedMultiplier ticks
        var speedFactor = _tickCounter <= MaxTicksForSpeedMultiplier ? 1.0 / SpeedMultiplier : 1.0;

        if (_spawnInterval == 0)
        {
            // todo make special platforms that spawn with special items like coins/hunger or bottles

            var platformWidth = 150;
            _platformManager
                .SpawnRandomPlatform(
                    _random.NextDouble() * (SystemParameters.FullPrimaryScreenWidth - platformWidth),
                    platformWidth, 30,
                    1);
            _spawnInterval = (int)(_random.Next(50, 120) * speedFactor);
        }
    }
}