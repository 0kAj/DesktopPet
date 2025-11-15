using System.Windows.Input;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class JumpControllerMovementState : IPetEvent
{
    public bool IsDone => false;

    private readonly bool _allowDoubleJump;
    private int _jumpCounter = 0;
    private readonly PetWindow _petWindow;
    private readonly PetBrain _brain;

    public JumpControllerMovementState(bool allowDoubleJump, PetBrain petBrain)
    {
        _allowDoubleJump = allowDoubleJump;
        _brain = petBrain;
        _petWindow = petBrain.PetWindow;

        // add Event
        _petWindow.KeyDown += Jump;
    }

    // jump if on ground or jumped once when DoubleJump
    private bool CanJump()
    {
        if (_brain.IsOnGround) return true;
        if (!_allowDoubleJump) return false;
        return _jumpCounter < 1; // it's not a BUG it's a feature!
    }

    public void OnUnregister()
    {
        _petWindow.KeyDown -= Jump;
    }

    private void Jump(object sender, KeyEventArgs e)
    {
        // _petWindow.debugLabel.Content = "Jump";
        if (e.Key != Key.Space) return;
        if (!CanJump()) return;

        if (_brain.IsOnGround)
            _jumpCounter = 0;
        else
            _jumpCounter++;

        _brain.IsOnGround = false;
        _petWindow.VelocityY = -5;
    }
}