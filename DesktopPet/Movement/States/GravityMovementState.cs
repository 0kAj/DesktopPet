using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class GravityMovementState : IBehaviourState
{
    private readonly double _targetY;
    private double _velocityY = 0;
    private const double Gravity = 9.81 / 100;
    private readonly PetWindow _petWindow;

    public GravityMovementState(PetWindow petWindow)
    {
        _petWindow = petWindow;

        var screenHeight = SystemParameters.WorkArea;
        _targetY = screenHeight.Bottom - petWindow.Height;
        
        _petWindow.IsOnGround = false;
    }

    public bool IsDone => _petWindow.IsOnGround;

    public void OnStart()
    {
        
    }

    public bool CanTick()
    {
        return !(_petWindow.IsOnGround || _petWindow.IsOnDragging);
    }

    public void Tick()
    {
        _velocityY += Gravity;
        _petWindow.Top += _velocityY;

        if (_petWindow.Top >= _targetY)
        {
            _petWindow.Top = _targetY;
            _velocityY = 0;
            _petWindow.IsOnGround = true;
            OnEnd();
        }
    }

    public void OnEnd()
    {
        
    }
}
