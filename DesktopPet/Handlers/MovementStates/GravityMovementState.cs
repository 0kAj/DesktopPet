using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class GravityMovementState : IBehaviourState
{
    private readonly PetBrain _brain;
    private readonly double _gravity;
    private readonly PetWindow _petWindow;

    public GravityMovementState(double gravity, PetBrain petBrain)
    {
        _gravity = gravity;
        _brain = petBrain;
        _petWindow = petBrain.PetWindow;

        _brain.IsOnGround = false;
    }

    public bool IsDone => _brain.IsOnGround;

    public bool CanTick() //todo BUG: can stop gravity by holding right-clicked mouse over pet
    {
        return !_brain.IsOnDragging;
    }

    public void Tick()
    {
        _petWindow.VelocityY += _gravity / 100;
        var collisionRect = _petWindow.GetCollisionRect();

        var landed = false;

        // Taskbar Collision:
        var targetY = SystemParameters.WorkArea.Bottom - collisionRect.Height - (collisionRect.Top - _petWindow.Top);

        if (_petWindow.Top >= targetY)
        {
            _petWindow.Top = targetY;
            _petWindow.VelocityY = 0;
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
                    _petWindow.VelocityY > 0) // only when falling
                {
                    // collision with platform
                    _petWindow.Top -= overlap;
                    _petWindow.VelocityY = platform.VelocityY;
                    landed = true;
                    break;
                }
            }

        // Status aktualisieren
        // _petWindow.debugLabel.Content = landed ? "Landed" : "Not landed";
        _brain.IsOnGround = landed;


        // Optional: Reset, wenn Pet nicht mehr auf Plattform/Boden
        if (!landed && _brain.IsOnGround) _brain.IsOnGround = false;
    }

    public void OnEnd()
    {
    }
}