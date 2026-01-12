using System.Net.Mime;
using System.Windows;
using System.Windows.Threading;
using DesktopPet.Data.Attributes;
using DesktopPet.Utils;
using Microsoft.Extensions.Hosting;

namespace DesktopPet.Background;

public class PetStatUpdater : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

    private PetAttribute? _hungerAttribute;
    private PetAttribute? _thirstAttribute;

    private bool _isSecondMinute;

    private string? _petName;

    public PetStatUpdater()
    {
    }

    public void SetPetName(string petName)
    {
        _petName = petName;

        PetAttributeHelper.InitStatsAttributes(petName, out var hunger, out var thirst);
        _hungerAttribute = hunger;
        _thirstAttribute = thirst;
    }

    private void UpdateStats()
    {
        if (_hungerAttribute == null || _thirstAttribute == null)
            return;

        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            if (_isSecondMinute)
            {
                var hunger = Parse(_hungerAttribute.Value);
                _hungerAttribute.Value = Math.Max(hunger - 1, 0).ToString();
            }

            var thirst = Parse(_thirstAttribute.Value);
            _thirstAttribute.Value = Math.Max(thirst - 1, 0).ToString();

            _isSecondMinute = !_isSecondMinute;
        });
    }

    private static int Parse(string value) => int.TryParse(value, out var integer) ? integer : 0;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Task.Delay(_interval, stoppingToken).Wait(stoppingToken);

            if (_petName != null)
                UpdateStats();
        }

        return Task.CompletedTask;
    }
}