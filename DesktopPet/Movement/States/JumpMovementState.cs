using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class JumpMovementState : IBehaviourState
{
    private Random _random = new Random();
    private int _jumpChance;
    private PetWindow _petWindow;

    public JumpMovementState(int jumpChance, PetWindow petWindow)
    {
        _jumpChance = jumpChance;
        _petWindow = petWindow;
    }

    public bool IsDone => false;

    public bool CanTick()
    {
        return _random.Next() % _jumpChance == 0;
    }

    public void Tick()
    {
        if (_petWindow.IsOnGround)
        {
            _petWindow.IsOnGround = false;
            _petWindow.VelocityY = -5;
        }
    }

    public void OnEnd()
    {
        
    }
}