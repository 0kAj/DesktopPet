using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.MovementStates;

public class JumpMovementState : IBehaviourState
{
    private readonly PetBrain _brain;
    private readonly int _jumpChance;
    private readonly PetViewModel _petViewModel;
    private readonly Random _random = new();

    public JumpMovementState(int jumpChance, PetBrain petBrain)
    {
        _jumpChance = jumpChance;
        _brain = petBrain;
        _petViewModel = petBrain.PetViewModel;
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
            _petViewModel.VelocityY = -5;
        }
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}