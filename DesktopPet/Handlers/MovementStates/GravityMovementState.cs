using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class GravityMovementState : IBehaviourState
{
    private const double Gravity = 9.81 / 100;
    private readonly PetWindow _petWindow;
    private readonly double _targetY;

    public GravityMovementState(PetWindow petWindow)
    {
        _petWindow = petWindow;

        var screenHeight = SystemParameters.WorkArea;
        _targetY = screenHeight.Bottom - petWindow.Height;

        _petWindow.IsOnGround = false;
    }

    public bool IsDone => _petWindow.IsOnGround;

    public bool CanTick()
    {
        return !(_petWindow.IsOnGround || _petWindow.IsOnDragging);
    }

    public void Tick()
    {
        _petWindow.VelocityY += Gravity;

        if (_petWindow.Top >= _targetY)
        {
            _petWindow.VelocityY = 0;
            _petWindow.IsOnGround = true;
            OnEnd();
        }
    }

    public void OnEnd()
    {
    }
}