namespace DesktopPet.Engine.GameObjects;

public abstract class TimedGameObject : WPFGameObject
{
    public abstract bool IsTicking { get; }
    protected event Action? TickStart;
    protected event Action? TickStop;

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