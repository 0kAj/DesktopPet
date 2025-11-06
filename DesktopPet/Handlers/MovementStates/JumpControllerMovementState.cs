using System.Windows.Input;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class JumpControllerMovementState : IPetEvent
{
    public bool IsDone => false;
    
    private bool AllowDoubleJump;
    private int _jumpCounter = 0;
    private PetWindow _petWindow;

    public JumpControllerMovementState(bool allowDoubleJump, PetWindow petWindow)
    {
        AllowDoubleJump = allowDoubleJump;
        _petWindow = petWindow;
        
        // add Event
        _petWindow.KeyDown += Jump;
    }

    // jump if on ground or jumped once when DoubleJump
    private bool CanJump()
    {
        return _petWindow.IsOnGround || (AllowDoubleJump && _jumpCounter < 2);
    }

    public void OnUnregister()
    {
        _petWindow.KeyDown -= Jump;
    }

    private void Jump(object sender, KeyEventArgs e)
    {
        // _petWindow.debugLabel.Content = "Jump";
        if (!CanJump()) return;
        
        if (_petWindow.IsOnGround)
            _jumpCounter = 0;
        else
            _jumpCounter++;
        
        _petWindow.IsOnGround = false;
        _petWindow.VelocityY = -5;
    }
}