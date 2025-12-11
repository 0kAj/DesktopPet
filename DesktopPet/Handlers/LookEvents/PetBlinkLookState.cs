using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetBlinkLookState : IBehaviourState
{
    Random _random = new Random();
    private LookingPetViewModel _lookingPet;

    private MultiTickAttribute<double> _eyeScaleYAttribute;
    private float _eyeScaleYAttributeAmount;
    private int _eyeScaleYAttributeDirection = 1;

    public PetBlinkLookState(LookingPetViewModel lookingPet)
    {
        _lookingPet = lookingPet;
        lookingPet.EyeScaleY = 1.0;
    }

    public bool IsDone => false;
    public bool CanTick()
    {
        var randomValue = _random.Next() % 500 == 0;
        if (_eyeScaleYAttribute == null)
            return randomValue;
        return _eyeScaleYAttribute.IsChanging || randomValue;
    }

    public void Tick()
    {
        // Scale
        if (_eyeScaleYAttribute == null)
        {
            _eyeScaleYAttribute = new(1.0, 0.1);
        }
        
        if (!_eyeScaleYAttribute.IsChanging)
            _eyeScaleYAttribute.IsChanging = true;


        if (_eyeScaleYAttribute.IsChanging)
        {
            _lookingPet.EyeScaleY = Single.Lerp((float)_eyeScaleYAttribute.StartValue, (float)_eyeScaleYAttribute.EndValue, _eyeScaleYAttributeAmount);
            _eyeScaleYAttributeAmount += 0.1f * _eyeScaleYAttributeDirection;
            
            if (_eyeScaleYAttributeAmount > 1)
                _eyeScaleYAttributeDirection *= -1;
            if (_eyeScaleYAttributeAmount < 0)
            {
                _eyeScaleYAttribute.IsChanging = false;
                _eyeScaleYAttributeDirection = 1; // reset dir
            }
        }
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}