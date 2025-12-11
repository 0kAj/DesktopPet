using System.Windows;
using DesktopPet.Engine;
using DesktopPet.Handlers;
using DesktopPet.Handlers.LookEvents;

namespace DesktopPet.WPF;

public partial class PetWindow : VelocityWindow
{
    private readonly PetBrain _brain;

    private LookingPetViewModel _vm;

    public PetWindow(string petName)
    {
        InitializeComponent();
        // give it a brain
        _vm = new LookingPetViewModel(this);
        DataContext = _vm;
        
        _brain = new PetBrain(this, _vm) { Name = petName };
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);

        DebugLabel.Content = _brain.Name;

        ContentRendered += (_, _) => StartTicking();

        // GameManager.Instance.StartGame("Pet Jump", _brain);
        // GameManager.Instance.StartGame("Food Collector", _brain);
        // new GameSelectorWindow(_brain).Show();
    }

    protected override void Tick()
    {
        base.Tick();
        _brain.Tick();
    }

    protected override void Tick(float delta)
    {
        // debugLabel.Content = delta;
    }

    public override Rect GetCollisionRect()
    {
        var res = Pet.PointToScreen(new Point(0, 0));
        return new Rect(res.X, res.Y, Pet.ActualWidth, Pet.ActualHeight);
    }

    public override Vector GetCollisionPositionVector()
    {
        var res = Pet.PointToScreen(new Point(0, 0));
        return new Vector(res.X +  Pet.ActualWidth / 2, res.Y);
    }
}