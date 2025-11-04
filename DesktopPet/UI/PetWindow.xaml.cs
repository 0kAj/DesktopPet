using System.Windows;
using DesktopPet.Handlers;
using DesktopPet.Handlers.Events;
using DesktopPet.Handlers.MovementStates;

namespace DesktopPet.UI;

public partial class PetWindow : VelocityWindow
{
    private PetMovementHandler _petMovementHandler;
    private PetEventHandler _petEventHandler;

    public PetWindow()
    {
        InitializeComponent();
        Speed = 3;
        _petMovementHandler = new PetMovementHandler(
            new GravityMovementState(this),
            new DragDropMovementState(this),
            new JumpMovementState(1000, this),
            new MoveToPositionMovementState(MoveToPositionMovementState.PositionState.Right, this));

        // add PetEventHandler
        _petEventHandler = new PetEventHandler(new PetActionContextMenuPetEvent(this));
    }

    public bool IsOnGround { get; set; }
    public bool IsOnDragging { get; set; }

    public double Speed { get; set; }

    protected override void Tick()
    {
        base.Tick();
        // lasse das Pet vom Zentrum auf die Taskleiste fallen
        // bewege nach irgendwo auf der Taskleiste
        // bewege es nach rechts
        _petMovementHandler.Tick();
    }
}