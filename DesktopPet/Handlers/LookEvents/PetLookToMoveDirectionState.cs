using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetLookToMoveDirectionState : IBehaviourState
{
    private readonly MultiTickAttribute _lookDirectionXAttribute;
    private readonly MultiTickAttribute _lookDirectionYAttribute;
    private readonly PetViewModel _petViewModel;

    public PetLookToMoveDirectionState(PetViewModel petViewModel)
    {
        _petViewModel = petViewModel;

        _lookDirectionXAttribute = new MultiTickAttribute(0.1);
        _lookDirectionYAttribute = new MultiTickAttribute(0.1);
    }

    public bool IsDone => false;

    public bool CanTick()
    {
        return _petViewModel.VelocityX != 0;
    }

    public void Tick()
    {
        var targetX = Math.Sign(_petViewModel.VelocityX) * 3;
        _petViewModel.LookDirectionX = Double.Lerp(_petViewModel.LookDirectionX, targetX, 0.1f);
        var targetY = Math.Sign(_petViewModel.VelocityY) * 3;
        _petViewModel.LookDirectionY = Double.Lerp(_petViewModel.LookDirectionY, targetY, 0.1f);
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}