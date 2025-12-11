using DesktopPet.Handlers.Events;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Handlers.MovementStates;
using DesktopPet.MiniGames;
using PetWindow = DesktopPet.WPF.PetWindow;

namespace DesktopPet.Handlers;

public class PetBrain
{
    public enum MovementTemplate //todo YOU can do this better!!!
    {
        DefaultPet,
        BasicPetController
    }

    private readonly PetEventHandler _petEventHandler;
    private readonly PetMovementHandler _petMovementHandler;
    
    private LookingPetViewModel _lookingPet;

    public PetBrain(PetWindow petWindow, LookingPetViewModel lookingPetViewModel)
    {
        PetWindow = petWindow;
        _lookingPet = lookingPetViewModel;

        _petMovementHandler = new PetMovementHandler();
        // add PetEventHandler
        _petEventHandler = new PetEventHandler();
    }

    public required string Name { get; set; }

    public PetWindow PetWindow { get; private set; }

    public bool IsOnGround { get; set; }
    public bool IsOnDragging { get; set; }

    public double Speed { get; set; }

    public PlatformManager? PlatformManager { get; set; }

    public void InitFromMovementTemplate(MovementTemplate template) //todo YOU can do this better!!!
    {
        // reset AI
        _petMovementHandler.ClearStates();
        _petEventHandler.ClearStates();
        
        // add universal states
        _petMovementHandler.AddState(new PetBlinkLookState(_lookingPet));
        _petMovementHandler.AddState(new PetLookToMoveDirectionState(_lookingPet));
        _petMovementHandler.AddState(new PetLookToMousePositionState(_lookingPet, PetWindow));

        
        // Set AI
        switch (template)
        {
            default:
            case MovementTemplate.DefaultPet:
                Speed = 3;
                _petMovementHandler.AddState(new GravityMovementState(9.81, this));
                _petMovementHandler.AddState(new DragDropMovementState(this));
                _petMovementHandler.AddState(new JumpMovementState(10000, this));
                _petMovementHandler.AddState(
                    new MoveToPositionMovementState(MoveToPositionMovementState.PositionState.Right, this));
                _petEventHandler.AddPetEvent(new PetActionContextMenuPetEvent(this));
                break;
            case MovementTemplate.BasicPetController:
                Speed = 5;
                _petMovementHandler.AddState(new GravityMovementState(12, this));
                _petMovementHandler.AddState(new MovementControllerMovementState(true, this));
                _petEventHandler.AddPetEvent(new JumpControllerMovementState(true, this));
                break;
        }
    }

    public void Tick()
    {
        _petMovementHandler.Tick();
    }
}