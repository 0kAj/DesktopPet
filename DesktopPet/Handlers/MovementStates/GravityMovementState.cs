using System.Windows;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;
using PetWindow = DesktopPet.WPF.PetWindow;

namespace DesktopPet.Handlers.MovementStates;

public class GravityMovementState : IBehaviourState
{
    private readonly PetBrain _brain;
    private readonly double _gravity;
    private readonly PetViewModel _petViewModel;

    public GravityMovementState(double gravity, PetBrain petBrain)
    {
        _gravity = gravity;
        _brain = petBrain;
        _petViewModel = petBrain.PetViewModel;

        _brain.IsOnGround = false;
    }

    public bool IsDone => _brain.IsOnGround;

    public bool CanTick()
    {
        return !_brain.IsOnDragging;
    }

    public void Tick()
    {
        _petViewModel.VelocityY += _gravity / 100;
        var collisionRect = _petViewModel.CollisionRect;

        var landed = false;

        // Taskbar Collision:
        var targetY = SystemParameters.WorkArea.Bottom - collisionRect.Height - (collisionRect.Top - _petViewModel.Top);

        if (_petViewModel.Top >= targetY)
        {
            _petViewModel.Top = targetY;
            _petViewModel.VelocityY = 0;
            landed = true;
        }

        // platforms
        if (_brain.PlatformManager != null && !landed)
            foreach (var platform in _brain.PlatformManager.Platforms)
            {
                var platformRect = platform.GetCollisionRect();

                var overlap = collisionRect.Bottom - platformRect.Top;

                const double tolerance = 10.0;
                if (collisionRect.Bottom >= platformRect.Top &&
                    collisionRect.Bottom <= platformRect.Top + tolerance &&
                    collisionRect.Top < platformRect.Top && // from top
                    collisionRect.Right > platformRect.Left &&
                    collisionRect.Left < platformRect.Right &&
                    _petViewModel.VelocityY > 0) // only when falling
                {
                    // collision with platform
                    _petViewModel.Top -= overlap;
                    _petViewModel.VelocityY = platform.DefaultVelocityY;
                    landed = true;
                    platform.OnPlayerContact(_brain);
                    break;
                }
            }

        // _petWindow.debugLabel.Content = landed ? "Landed" : "Not landed";
        _brain.IsOnGround = landed;

        if (!landed && _brain.IsOnGround) _brain.IsOnGround = false;
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}