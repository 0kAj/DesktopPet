using DesktopPet.Data.Attributes;
using DesktopPet.Utils;

namespace DesktopPet.Background;

public class PetStatUpdater
{
    private bool _doTick;

    private PetAttribute _hungerAttribute;

    private bool _isSecondMinute;

    private string _petName;
    private PetAttribute _thirstAttribute;

    public PetStatUpdater()
    {
    }

    public PetStatUpdater(string petName)
    {
        PetAttributeHelper.InitStatsAttributes(petName, out _hungerAttribute, out _thirstAttribute);
    }

    public static PetStatUpdater Instance { get; } = new();

    public string PetName
    {
        get => _petName;
        set
        {
            _doTick = false;
            _petName = value;
            PetAttributeHelper.InitStatsAttributes(value, out _hungerAttribute, out _thirstAttribute);
            StartAsync();
        }
    }

    private async void StartAsync()
    {
        _doTick = true;
        while (_doTick)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            Timer_Tick();
        }
    }

    private void Timer_Tick()
    {
        if (_isSecondMinute)
        {
            var hunger = int.TryParse(_hungerAttribute.Value, out var hungerValue) ? hungerValue : 0;
            _hungerAttribute.Value = Math.Max(hunger - 1, 0).ToString();
        }

        var thirst = int.TryParse(_thirstAttribute.Value, out var thirstValue) ? thirstValue : 0;
        _thirstAttribute.Value = Math.Max(thirst - 1, 0).ToString();

        _isSecondMinute = !_isSecondMinute;
    }
}