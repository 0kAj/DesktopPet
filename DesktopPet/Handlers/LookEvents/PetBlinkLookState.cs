using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.LookEvents;

public class PetBlinkLookState : IBehaviourState
{
    Random _random = new Random();
    private LookingPetViewModel _lookingPet;

    private MultiTickAttribute<double> _eyeScaleYAttribute;
    private bool _closeEyes = false;

    public PetBlinkLookState(LookingPetViewModel lookingPet)
    {
        _lookingPet = lookingPet;
        lookingPet.EyeScaleY = 1.0;

        _eyeScaleYAttribute = new MultiTickAttribute<double>(0.1, true);
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

        var targetYScale = _closeEyes ? 0.1 : 1.0;

        _lookingPet.EyeScaleY = _eyeScaleYAttribute.Tick(_lookingPet.EyeScaleY, targetYScale);

        if (Math.Abs(_lookingPet.EyeScaleY - targetYScale) < 0.01)
            _closeEyes = !_closeEyes;
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }
}