using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

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

    private readonly PetWindow _petWindow;
    private readonly PositionState _targetPositionState;

    public MoveToPositionMovementState(PositionState targetPositionState, PetBrain petBrain)
    {
        _targetPositionState = targetPositionState;
        _brain = petBrain;
        _petWindow = petBrain.PetWindow;
    }

    public bool IsDone => Math.Abs((GetTargetPosition() - _petWindow.GetPositionVector()).Length) <= 10;

    public bool CanTick()
    {
        return _brain.IsOnGround;
    }

    public void Tick()
    {
        var direction = GetTargetPosition() - _petWindow.GetPositionVector();
        direction.Normalize();
        direction *= _brain.Speed;
        _petWindow.VelocityX = direction.X;
        _petWindow.VelocityY = direction.Y;
    }

    public void OnEnd()
    {
        // reset velocity
        _petWindow.ResetVelocity();
    }

    private Vector GetTargetPosition()
    {
        // target y = taskbar y
        var targetY = SystemParameters.WorkArea.Bottom - _petWindow.ActualHeight;

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