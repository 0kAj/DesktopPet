using DesktopPet.Movement;
using DesktopPet.Movement.States;

namespace DesktopPet.UI;

public partial class PetWindow : TickingWindow
{
    private readonly PetMovementHandler _petMovementHandler;
    
    public bool IsOnGround { get; set; }
    public bool IsOnDragging { get; set; }
    
    public PetWindow()
    {
        InitializeComponent();
        _petMovementHandler = new PetMovementHandler(
            new GravityMovementState(this),
            new MovingMovementState(this),
            new DragDropMovementState(this));
    }

    protected override void Tick()
    {
        // lasse das Pet vom Zentrum auf die Taskleiste fallen
        // bewege nach irgendwo auf der Taskleiste
        // bewege es nach rechts
        _petMovementHandler.Tick();
    }
}