using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;
using PetWindow = DesktopPet.WPF.PetWindow;

namespace DesktopPet.Handlers.MovementStates;

public class MovementControllerMovementState : IBehaviourState
{
    private readonly bool _allowAirControl;
    private readonly PetBrain _brain;
    private readonly PetViewModel _petViewModel;

    public MovementControllerMovementState(bool allowAirControl, PetBrain petBrain)
    {
        _allowAirControl = allowAirControl;
        _brain = petBrain;
        _petViewModel = petBrain.PetViewModel;
    }

    public bool IsDone => false;

    public bool CanTick()
    {
        return _allowAirControl ? true : _brain.IsOnGround;
    }

    public void Tick()
    {
        // _petWindow.debugLabel.Content = Helper.GetMousePosition().ToString();
        var distance = Helper.GetMousePosition().X - _petViewModel.Left - _petViewModel.WindowWidth / 2;

        // only move if position not reached
        if (Math.Abs(distance) < 5)
            _petViewModel.VelocityX = 0;
        else
            _petViewModel.VelocityX = Math.Sign(distance) * _brain.Speed;
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}