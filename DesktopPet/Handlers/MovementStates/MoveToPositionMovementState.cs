using System.Windows;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.MovementStates;

public class MoveToPositionMovementState : IBehaviourState
{
    public enum PositionState
    {
        Left,
        Right,
        Center
    }

    private readonly PetBrain _brain;

    private readonly PetViewModel _petViewModel;
    private readonly PositionState _targetPositionState;

    public MoveToPositionMovementState(PositionState targetPositionState, PetBrain petBrain)
    {
        _targetPositionState = targetPositionState;
        _brain = petBrain;
        _petViewModel = petBrain.PetViewModel;
    }

    public bool IsDone => Math.Abs((GetTargetPosition() - _petViewModel.GetPositionVector()).Length) <= 10;

    public bool CanTick()
    {
        return _brain.IsOnGround;
    }

    public void Tick()
    {
        var direction = GetTargetPosition() - _petViewModel.GetPositionVector();
        direction.Normalize();
        direction *= _brain.Speed;
        _petViewModel.VelocityX = direction.X;
        _petViewModel.VelocityY = direction.Y;
    }

    public void OnEnd()
    {
        // reset velocity
        _petViewModel.ResetVelocity();
    }

    public void UnRegister()
    {
    }

    private Vector GetTargetPosition()
    {
        // target y = taskbar y
        var targetY = SystemParameters.WorkArea.Bottom - _petViewModel.CollisionRect.Height -
                      (_petViewModel.CollisionRect.Top - _petViewModel.Top);
        // var targetY = SystemParameters.WorkArea.Bottom - _petViewModel.CollisionRect.Height - _petViewModel.CollisionRect.Top;

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
}