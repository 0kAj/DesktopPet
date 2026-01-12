using System.Windows.Input;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;
using DesktopPet.WPF;

namespace DesktopPet.Handlers.MovementStates;

public class JumpControllerMovementState : IPetEvent
{
    private readonly bool _allowDoubleJump;
    private readonly PetBrain _brain;

    private readonly PetEventManager _eventManager;
    private readonly PetViewModel _petViewModel;
    private int _jumpCounter;

    public JumpControllerMovementState(PetEventManager eventManager, PetBrain petBrain, bool allowDoubleJump)
    {
        _eventManager = eventManager;
        _allowDoubleJump = allowDoubleJump;
        _brain = petBrain;
        _petViewModel = petBrain.PetViewModel;

        // add Event
        _eventManager.KeyDown += Jump;
    }

    public bool IsDone => false;

    public void OnUnregister()
    {
        _eventManager.KeyDown -= Jump;
    }

    // jump if on ground or jumped once when DoubleJump
    private bool CanJump()
    {
        if (_brain.IsOnGround) return true;
        if (!_allowDoubleJump) return false;
        return _jumpCounter < 1; // it's not a BUG it's a feature!
    }

    private void Jump(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Space) return;
        if (!CanJump()) return;

        if (_brain.IsOnGround)
            _jumpCounter = 0;
        else
            _jumpCounter++;

        _brain.IsOnGround = false;
        _petViewModel.VelocityY = -5;
    }
}