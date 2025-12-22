using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetLookToMoveDirectionState : IBehaviourState
{
    private readonly MultiTickAttribute _lookDirectionXAttribute;
    private readonly MultiTickAttribute _lookDirectionYAttribute;
    private readonly PetViewModel _lookingPet;

    public PetLookToMoveDirectionState(PetViewModel lookingPet)
    {
        _lookingPet = lookingPet;

        _lookDirectionXAttribute = new MultiTickAttribute(0.1);
        _lookDirectionYAttribute = new MultiTickAttribute(0.1);
    }

    public bool IsDone => false;

    public bool CanTick()
    {
        return _lookingPet.VelocityX != 0;
    }

    public void Tick()
    {
        var targetX = Math.Sign(_lookingPet.VelocityX) * 3;
        _lookingPet.LookDirectionX = _lookDirectionXAttribute.Tick(_lookingPet.LookDirectionX, targetX);
        var targetY = Math.Sign(_lookingPet.VelocityY) * 3;
        _lookingPet.LookDirectionY = _lookDirectionYAttribute.Tick(_lookingPet.LookDirectionY, targetY);
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}