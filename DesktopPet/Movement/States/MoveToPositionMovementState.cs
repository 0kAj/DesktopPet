using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class MoveToPositionMovementState : IBehaviourState
{
    private PositionState _targetPositionState;
    
    private PetWindow _petWindow;

    public MoveToPositionMovementState(PositionState targetPositionState, PetWindow petWindow)
    {
        _targetPositionState = targetPositionState;
        _petWindow = petWindow;
    }

    public bool IsDone => Math.Abs((GetTargetPosition() - _petWindow.GetPositionVector()).Length) <= 10;
    public bool CanTick() => _petWindow.IsOnGround;

    public void Tick()
    {
        var direction = GetTargetPosition() - _petWindow.GetPositionVector();
        direction.Normalize();
        direction *= _petWindow.Speed;
        _petWindow.VelocityX = direction.X;
        _petWindow.VelocityY = direction.Y;
    }

    private Vector GetTargetPosition()
    {
        // target y = taskbar y
        var targetY = SystemParameters.WorkArea.Bottom - _petWindow.Height;
        
        // target x = CENTER, LEFT, RIGHT
        switch (_targetPositionState)
        {
            case PositionState.Center:
                return new Vector(
                    SystemParameters.PrimaryScreenWidth / 2,
                    targetY);
            case PositionState.Left:
                return new Vector(
                    100f,
                    targetY);
            default:
            case PositionState.Right:
                return new Vector(
                    SystemParameters.PrimaryScreenWidth - 100f,
                    targetY);
        }
    }

    public void OnEnd()
    {
        // reset velocity
        _petWindow.ResetVelocity();
    }

    public enum PositionState
    {
        Left,
        Right,
        Center
    }
}