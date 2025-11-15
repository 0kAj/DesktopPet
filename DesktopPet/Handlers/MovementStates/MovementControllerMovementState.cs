using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class MovementControllerMovementState : IBehaviourState
{
    private readonly bool _allowAirControl;
    private readonly PetBrain _brain;
    private readonly PetWindow _petWindow;

    public MovementControllerMovementState(bool allowAirControl, PetBrain petBrain)
    {
        _allowAirControl = allowAirControl;
        _brain = petBrain;
        _petWindow = petBrain.PetWindow;
    }

    public bool IsDone => false;

    public bool CanTick()
    {
        return _allowAirControl ? true : _brain.IsOnGround;
    }

    public void Tick()
    {
        // _petWindow.debugLabel.Content = Helper.GetMousePosition().ToString();
        var distance = Helper.GetMousePosition().X - _petWindow.Left - _petWindow.Width / 2;

        // only move if position not reached
        if (Math.Abs(distance) < 5)
            _petWindow.VelocityX = 0;
        else
            _petWindow.VelocityX = Math.Sign(distance) * _brain.Speed;
    }

    public void OnEnd()
    {
    }
}