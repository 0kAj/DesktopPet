using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetLookToMousePositionState : IBehaviourState
{
    private LookingPetViewModel _lookingPet;
    private IWindowHelper _windowHelper;
    
    private MultiTickAttribute<double> _lookDirectionXAttribute;
    private MultiTickAttribute<double> _lookDirectionYAttribute;

    public PetLookToMousePositionState(LookingPetViewModel lookingPet, IWindowHelper windowHelper)
    {
        _lookingPet = lookingPet;
        _windowHelper = windowHelper;
        
        _lookDirectionXAttribute = new MultiTickAttribute<double>(0.1);
        _lookDirectionYAttribute = new MultiTickAttribute<double>(0.1);
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

        var targetX = Math.Clamp(dirX, -3, 3);
        var targetY = Math.Clamp(dirY, -3, 3);
        
        _lookingPet.LookDirectionX = _lookDirectionXAttribute.Tick(_lookingPet.LookDirectionX, targetX);
        _lookingPet.LookDirectionY = _lookDirectionYAttribute.Tick(_lookingPet.LookDirectionY, targetY);
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}