namespace DesktopPet;

public abstract class TimedWindow : Window
{
    protected event Action TickStart;
    protected event Action TickStop;
    
    public abstract bool IsTicking { get; }
    
    public abstract void StartTicking();
    public abstract void StopTicking();

    protected void OnTickStart()
    {
        TickStart?.Invoke();
    }

    protected void OnTickStop()
    {
        TickStop?.Invoke();
    }
    
    protected abstract void Tick();
    protected abstract void Tick(float delta);
}