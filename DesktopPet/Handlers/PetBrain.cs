using DesktopPet.Handlers.Events;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Handlers.MovementStates;
using DesktopPet.MiniGames;
using DesktopPet.WPF;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopPet.Handlers;

public class PetBrain
{
    public enum MovementTemplate
    {
        DefaultPet,
        BasicPetController
    }

    private readonly PetEventHandler _petEventHandler;
    private readonly PetMovementHandler _petMovementHandler;

    public readonly PetViewModel PetViewModel;

    public PetBrain(PetViewModel petViewModel)
    {
        PetViewModel = petViewModel;

        _petMovementHandler = new PetMovementHandler();
        // add PetEventHandler
        _petEventHandler = new PetEventHandler();
    }

    public required string Name { get; set; }

    public bool IsOnGround { get; set; }
    public bool IsOnDragging { get; set; }

    public double Speed { get; set; }

    public PlatformManager? PlatformManager { get; set; }

    public void InitFromMovementTemplate(MovementTemplate template)
    {
        // reset AI
        _petMovementHandler.ClearStates();
        _petEventHandler.ClearStates();

        // add universal states
        _petMovementHandler.AddState(new PetBlinkLookState(PetViewModel));
        _petMovementHandler.AddState(new PetLookToMoveDirectionState(PetViewModel));
        _petMovementHandler.AddState(new PetLookToMousePositionState(PetViewModel));

        var eventManager = App.Host.Services.GetRequiredService<PetEventManager>();

        // Set AI
        switch (template)
        {
            default:
            case MovementTemplate.DefaultPet:
                Speed = 3;
                _petMovementHandler.AddState(new GravityMovementState(9.81, this));
                _petMovementHandler.AddState(new DragDropMovementState(this, eventManager));
                _petMovementHandler.AddState(new JumpMovementState(10000, this));
                _petMovementHandler.AddState(
                    new MoveToPositionMovementState(MoveToPositionMovementState.PositionState.Right, this));
                _petEventHandler.AddPetEvent(new PetActionContextMenuPetEvent(this));
                break;
            case MovementTemplate.BasicPetController:
                Speed = 5;
                _petMovementHandler.AddState(new GravityMovementState(12, this));
                _petMovementHandler.AddState(new MovementControllerMovementState(true, this));
                _petEventHandler.AddPetEvent(new JumpControllerMovementState(eventManager, this, true));
                break;
        }
    }

    public void Tick()
    {
        _petMovementHandler.Tick();
    }
}