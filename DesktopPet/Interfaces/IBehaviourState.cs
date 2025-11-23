namespace DesktopPet.Interfaces;

public interface IBehaviourState
{
    bool IsDone { get; }
    bool CanTick();
    void Tick();
    void OnEnd();
    void UnRegister();
}