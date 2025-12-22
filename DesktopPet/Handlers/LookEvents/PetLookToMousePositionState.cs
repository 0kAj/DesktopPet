using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetLookToMousePositionState : IBehaviourState
{
    private readonly MultiTickAttribute _lookDirectionXAttribute;
    private readonly MultiTickAttribute _lookDirectionYAttribute;
    private readonly PetViewModel _petViewModel;

    public PetLookToMousePositionState(PetViewModel lookingPet)
    {
        _petViewModel = lookingPet;

        _lookDirectionXAttribute = new MultiTickAttribute(0.1);
        _lookDirectionYAttribute = new MultiTickAttribute(0.1);
    }

    public bool IsDone => _petViewModel.VelocityX == 0;

    public bool CanTick()
    {
        return _petViewModel.VelocityX == 0;
    }

    public void Tick()
    {
        var point = Helper.GetMousePosition();

        var currentPos = _petViewModel.CollisionPositionVector;

        var dirX = point.X - currentPos.X;
        var dirY = point.Y - currentPos.Y;

        var targetX = Math.Clamp(dirX, -3, 3);
        var targetY = Math.Clamp(dirY, -3, 3);

        _petViewModel.LookDirectionX = _lookDirectionXAttribute.Tick(_petViewModel.LookDirectionX, targetX);
        _petViewModel.LookDirectionY = _lookDirectionYAttribute.Tick(_petViewModel.LookDirectionY, targetY);
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}