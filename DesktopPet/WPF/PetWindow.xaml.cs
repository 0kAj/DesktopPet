using System.Windows;
using DesktopPet.Engine;
using DesktopPet.Handlers;
using DesktopPet.Handlers.LookEvents;

namespace DesktopPet.WPF;

public partial class PetWindow : VelocityWindow
{
    private readonly PetBrain _brain;

    private readonly PetEventManager _eventManager;

    public PetWindow(PetEventManager eventManager, string petName)
    {
        InitializeComponent();
        _eventManager = eventManager;
        // give it a brain
        var vm = new LookingPetViewModel(this, petName);
        DataContext = vm;

        _brain = new PetBrain(this, vm) { Name = petName };
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);

        DebugLabel.Content = _brain.Name;

        ContentRendered += (_, _) => StartTicking();
        KeyDown += _eventManager.OnKeyDown;
        MouseDown += _eventManager.OnMouseDown;
        MouseUp += _eventManager.OnMouseUp;
        _eventManager.Pause += StopTicking;
        _eventManager.Resume += StartTicking;
        _eventManager.CaptureMouse += () => CaptureMouse();
        _eventManager.ReleaseMouseCapture += ReleaseMouseCapture;
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
        return new Vector(res.X + Pet.ActualWidth / 2, res.Y);
    }
}