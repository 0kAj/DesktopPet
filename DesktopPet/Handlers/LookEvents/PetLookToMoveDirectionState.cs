using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetLookToMoveDirectionState : IBehaviourState
{
    private LookingPetViewModel _lookingPet;

    public PetLookToMoveDirectionState(LookingPetViewModel lookingPet)
    {
        _lookingPet = lookingPet;
    }

    public bool IsDone => false;
    public bool CanTick()
    {
        return _lookingPet.VelocityX != 0;
    }

    public void Tick()
    {
        _lookingPet.LookDirectionX = Math.Sign(_lookingPet.VelocityX) * 3;
        _lookingPet.LookDirectionY = Math.Sign(_lookingPet.VelocityY) * 3;
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}