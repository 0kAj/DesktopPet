using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetLookToMousePositionState : IBehaviourState
{
    private LookingPetViewModel _lookingPet;
    private IWindowHelper _windowHelper;

    public PetLookToMousePositionState(LookingPetViewModel lookingPet, IWindowHelper windowHelper)
    {
        _lookingPet = lookingPet;
        _windowHelper = windowHelper;
    }

    public bool IsDone => _lookingPet.VelocityX == 0;
    public bool CanTick()
    {
        return _lookingPet.VelocityX == 0;
    }

    public void Tick()
    {
        var point = Helper.GetMousePosition();

        var currentPos = _windowHelper.GetCollisionPositionVector();
        
        var dirX = point.X - currentPos.X;
        var dirY = point.Y - currentPos.Y;
        
        _lookingPet.LookDirectionX = Math.Clamp(dirX, -3, 3);
        _lookingPet.LookDirectionY = Math.Clamp(dirY, -3, 3);
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}