using System.Windows;
using DesktopPet.Handlers;
using DesktopPet.Handlers.Events;
using DesktopPet.Handlers.MovementStates;

namespace DesktopPet.UI;

public partial class PetWindow : VelocityWindow
{
    private readonly PetEventHandler _petEventHandler;
    private readonly PetMovementHandler _petMovementHandler;

    public PetWindow()
    {
        InitializeComponent();
        _petMovementHandler = new PetMovementHandler();
        // add PetEventHandler
        _petEventHandler = new PetEventHandler();
        
        InitFromTemplate(MovementTemplate.DefaultPet);
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

    public Rect GetPetRect()
    {
        var transform = pet.TransformToAncestor(this); // transform pet to window
        var topLeft = transform.Transform(new Point(0, 0)); // transform pet in window to (0,0) top left corner of pet

        // add petposition (relative) to windowPos (absolute) -> petposition (absolute)
        double screenX = Left + topLeft.X;
        double screenY = Top + topLeft.Y;

        return new Rect(screenX, screenY, pet.ActualWidth, pet.ActualHeight);
    }
    
    
    //TODO move MovementTemplate and InitFromTemplate to external class -> PetAI/ Pet Brain
    public enum MovementTemplate
    {
        DefaultPet,
        BasicPetController
    }
    
    public void InitFromTemplate(MovementTemplate template)
    {
        // reset AI
        _petMovementHandler.ClearStates();
        _petEventHandler.ClearStates();
        // Set AI
        switch (template)
        {
            default:
            case MovementTemplate.DefaultPet:
                Speed = 3;
                _petMovementHandler.AddState(new GravityMovementState(this));
                _petMovementHandler.AddState(new DragDropMovementState(this));
                _petMovementHandler.AddState(new JumpMovementState(10000, this));
                _petMovementHandler.AddState(new MoveToPositionMovementState(MoveToPositionMovementState.PositionState.Right, this));
                _petEventHandler.AddPetEvent(new PetActionContextMenuPetEvent(this));
                break;
            case MovementTemplate.BasicPetController:
                Speed = 5;
                _petMovementHandler.AddState(new GravityMovementState(this));
                _petMovementHandler.AddState(new MovementControllerMovementState(true, this));
                _petEventHandler.AddPetEvent(new JumpControllerMovementState(true, this));
                break;
        }
    }
}