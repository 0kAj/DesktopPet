using System.Windows.Controls;
using DesktopPet.Attribute;
using DesktopPet.Engine;
using DesktopPet.Handlers;
using DesktopPet.WPF.WindowViewModels;

namespace DesktopPet.WPF.GameWindows;

[MiniGame("Food Collector")]
public partial class FoodCollectorMiniGameWindow : MiniGameWindow
{
    private readonly MiniGameViewModel _vm;

    public FoodCollectorMiniGameWindow(PetBrain petBrain) : base(petBrain)
    {
        InitializeComponent();
        SizingHelper.FitToScreen(this);

        _vm = new MiniGameViewModel(petBrain);
        DataContext = _vm;
        _vm.GameFinished += End;
        _vm.AddRemainingTime += amount => TDisplay.Timer.AddRemaining(amount);

        Loaded += (_, _) => _vm.SetCanvasSize(GameCanvas.ActualWidth, GameCanvas.ActualHeight);

        // route all key events to petwindow
        KeyDown += (_, args) => Brain.PetWindow.RaiseEvent(args);

        SetDelta(20);

        TDisplay.Timer.Set(30);
        TDisplay.Timer.Timeout += End;

        TickStart += () => TDisplay.Timer.Start();
        TickStop += () => TDisplay.Timer.Stop();
    }

    public override string GameName => "Food Collector";
    protected override Canvas MiniGameUiCanvas => UiCanvas;

    public override void Start()
    {
        // init Pet as playable
        Brain.InitFromMovementTemplate(PetBrain.MovementTemplate.BasicPetController);

        StartTicking();
    }

    protected override void End()
    {
        base.End();
        StopTicking();
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
        if (!IsTicking) return;
        _vm.Tick();
    }
}