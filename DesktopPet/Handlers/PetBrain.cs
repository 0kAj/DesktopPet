using DesktopPet.Handlers.Events;
using DesktopPet.Handlers.MovementStates;
using DesktopPet.MiniGames;
using DesktopPet.UI;

namespace DesktopPet.Handlers;

public class PetBrain
{
    private readonly PetEventHandler _petEventHandler;
    private readonly PetMovementHandler _petMovementHandler;

    public PetWindow PetWindow { get; private set; }
    
    public bool IsOnGround { get; set; }
    public bool IsOnDragging { get; set; }

    public double Speed { get; set; }

    public PlatformManager? PlatformManager { get; set; }

    public PetBrain(PetWindow petWindow)
    {
        PetWindow = petWindow;
        
        _petMovementHandler = new PetMovementHandler();
        // add PetEventHandler
        _petEventHandler = new PetEventHandler();
    }

    public enum MovementTemplate
    {
        DefaultPet,
        BasicPetController
    }
    
    public void InitFromMovementTemplate(MovementTemplate template)
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
                _petMovementHandler.AddState(new GravityMovementState(9.81,this));
                _petMovementHandler.AddState(new DragDropMovementState(this));
                _petMovementHandler.AddState(new JumpMovementState(10000, this));
                _petMovementHandler.AddState(new MoveToPositionMovementState(MoveToPositionMovementState.PositionState.Right, this));
                _petEventHandler.AddPetEvent(new PetActionContextMenuPetEvent(this));
                break;
            case MovementTemplate.BasicPetController:
                Speed = 5;
                _petMovementHandler.AddState(new GravityMovementState(12,this));
                _petMovementHandler.AddState(new MovementControllerMovementState(true, this));
                _petEventHandler.AddPetEvent(new JumpControllerMovementState(true, this));
                _petMovementHandler.AddState(new DragDropMovementState(this));
                break;
        }
    }

    public void Tick()
    {
        _petMovementHandler.Tick();
    }
}