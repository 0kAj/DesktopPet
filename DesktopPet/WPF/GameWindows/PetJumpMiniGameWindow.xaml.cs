using System.Windows.Controls;
using DesktopPet.Attribute;
using DesktopPet.Engine;
using DesktopPet.Handlers;
using DesktopPet.WPF.WindowViewModels;

namespace DesktopPet.WPF.GameWindows;

[MiniGame("Pet Jump")]
public partial class PetJumpMiniGameWindow : MiniGameWindow
{
    private readonly PetJumpViewModel _vm;

    public PetJumpMiniGameWindow(PetBrain petBrain) : base(petBrain)
    {
        InitializeComponent();
        SizingHelper.FitToScreen(this);

        _vm = new PetJumpViewModel(petBrain);
        DataContext = _vm;
        _vm.GameFinished += End;
        _vm.AddRemainingTime += amount => TDisplay.Timer.AddRemaining(amount);

        Loaded += (_, _) => _vm.SetCanvasSize(GameCanvas.ActualWidth, GameCanvas.ActualHeight);

        // route all key events to petwindow
        KeyDown += (_, e) => petBrain.PetWindow.RaiseEvent(e);

        SetDelta(20);

        TDisplay.Timer.Set(30);
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
        _vm.Tick();
    }
}