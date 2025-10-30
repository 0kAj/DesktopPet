using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class GravityMovementState : IBehaviourState
{
    private double _targetY;
    private const double Gravity = 9.81 / 100;
    private PetWindow _petWindow;

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
