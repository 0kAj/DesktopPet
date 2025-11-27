namespace DesktopPet.Engine;

public abstract class TimedWindow : Window
{
    public abstract bool IsTicking { get; }
    protected event Action TickStart;
    protected event Action TickStop;

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