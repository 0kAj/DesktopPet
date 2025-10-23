using System.Windows;

namespace DesktopPet;

public abstract class TickingWindow : Window
{
    protected bool DoTick { get; set; }
    private bool _isTicking;
    DateTime lastUpdate;

    protected TickingWindow()
    {
        DoTick = true;
        Loaded += (sender, args) => StartTick();
        lastUpdate = DateTime.Now;
    }

    protected async void StartTick()
    {
        if (_isTicking)
            return;
        
        _isTicking = true;
        
        while (DoTick)
        {
            // Zeitdifferenz berechnen (in Sekunden)
            var now = DateTime.Now;
            double deltaTime = (now - lastUpdate).TotalSeconds;
            lastUpdate = now;
            
            Tick(deltaTime);
            await Task.Delay(10);
        }
        _isTicking = false;
    }
    
    protected abstract void Tick(double deltaTime);
}