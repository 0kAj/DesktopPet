using DesktopPet.Handlers.Events;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Handlers.MovementStates;
using DesktopPet.MiniGames;
using DesktopPet.WPF;
using Microsoft.Extensions.DependencyInjection;
using PetWindow = DesktopPet.WPF.PetWindow;

namespace DesktopPet.Handlers;

public class PetBrain //todo Brain rework
{
    public enum MovementTemplate //todo YOU can do this better!!!
    {
        DefaultPet,
        BasicPetController
    }

    private readonly LookingPetViewModel _lookingPet;

    private readonly PetEventHandler _petEventHandler;
    private readonly PetMovementHandler _petMovementHandler;

    public PetBrain(PetWindow petWindow, LookingPetViewModel lookingPetViewModel)
    {
        PetWindow = petWindow;
        _lookingPet = lookingPetViewModel;

        _petMovementHandler = new PetMovementHandler();
        // add PetEventHandler
        _petEventHandler = new PetEventHandler();
    }

    public required string Name { get; set; }

    public PetWindow PetWindow { get; } //todo is this necessary?:
    
    //################# what is it doing?? #################################
    //# VOID KeyDown event register/unregister
    //# VOID Ticking start/Stop
    //# SET applying velocityY /X
    //# VOID raise Event -> events path through
    // RECT GetCollisionRect
    
    //####################### be in States ####################################################
    //# VOID Moue up/down event register/ unregister
    //# SET window position -> Left,Top
    // VECTOR2 GetDPISave global mouse
    //# VOID reset Velocity
    //# VOID capture and release mouse
    //# DOUBLE GET Top, Left
    // VECTOR2 PositionVector
    //# DOUBLE get actualheight
    
    // ##################### resulting Interfaces ########################
    
    // [DONE!] PetEventManager Singleton 
    // - EVENTS:
    //      - mouseUp/down keyUp/down
    //      - capture and release mouse
    // - TIMER start/Stop
    
    // IVelocity: -> MVVM Toolkit Messenger
    // - Velocity GET/SET
    
    // IWindowPosition: -> MVVM -> X,Y,Height
    // - position LEFT,TOP actualheight
    
    // IWindowHelper:
    // - GetDPISave
    // - PositionVector
    
    // missing :
    // GetCollisionRect
    

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