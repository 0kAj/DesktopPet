using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class JumpMovementState(int jumpChance, PetWindow petWindow) : IBehaviourState
{
    private Random _random = new Random();

    public bool IsDone => false;

    public bool CanTick()
    {
        return _random.Next() % 6 == 0;
    }

    public void Tick()
    {
        
    }

    public void OnEnd()
    {
        
    }
}