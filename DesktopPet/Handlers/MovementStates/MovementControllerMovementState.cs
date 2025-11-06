using System.Windows.Input;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class MovementControllerMovementState : IBehaviourState
{
    private bool _requireOnGround;
    private PetWindow _petWindow;

    public MovementControllerMovementState(bool requireOnGround, PetWindow petWindow)
    {
        _requireOnGround = requireOnGround;
        _petWindow = petWindow;
    }

    public bool IsDone => false;
    public bool CanTick() => _requireOnGround ? _petWindow.IsOnGround : true;

    public void Tick()
    {
        // _petWindow.debugLabel.Content = Helper.GetMousePosition().ToString();
        var distance =  Helper.GetMousePosition().X - _petWindow.Left - _petWindow.Width / 2;

        // only move if position not reached
        if (Math.Abs(distance) < 5)
            _petWindow.VelocityX = 0;
        else
            _petWindow.VelocityX = Math.Sign(distance)* _petWindow.Speed;
    }

    public void OnEnd()
    {
    }
}