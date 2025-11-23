using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class JumpMovementState : IBehaviourState
{
    private readonly PetBrain _brain;
    private readonly int _jumpChance;
    private readonly PetWindow _petWindow;
    private readonly Random _random = new();

    public JumpMovementState(int jumpChance, PetBrain petBrain)
    {
        _jumpChance = jumpChance;
        _brain = petBrain;
        _petWindow = petBrain.PetWindow;
    }

    public bool IsDone => false;

    public bool CanTick()
    {
        return _brain.IsOnGround && _random.Next() % _jumpChance == 0;
    }

    public void Tick()
    {
        if (_brain.IsOnGround)
        {
            _brain.IsOnGround = false;
            _petWindow.VelocityY = -5;
        }
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}