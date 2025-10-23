using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class GravityMovementState : IBehaviourState
{
    private double _targetY;
    private double _velocityY = 0;
    private const double Gravity = 9.81 * 30;
    private readonly PetWindow _petWindow;

    public GravityMovementState(PetWindow petWindow)
    {
        this._petWindow = petWindow;

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
        return true;
    }

    public void Tick(double deltaTime)
    {
        _velocityY += Gravity * deltaTime;
        _petWindow.Top += _velocityY * deltaTime;

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
