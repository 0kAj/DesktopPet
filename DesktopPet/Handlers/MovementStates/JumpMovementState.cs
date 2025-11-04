using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class JumpMovementState : IBehaviourState
{
    private readonly int _jumpChance;
    private readonly PetWindow _petWindow;
    private readonly Random _random = new();

    public JumpMovementState(int jumpChance, PetWindow petWindow)
    {
        _jumpChance = jumpChance;
        _petWindow = petWindow;
    }

    public bool IsDone => false;

    public bool CanTick()
    {
        return _petWindow.IsOnGround && _random.Next() % _jumpChance == 0;
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