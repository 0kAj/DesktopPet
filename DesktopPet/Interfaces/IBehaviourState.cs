using DesktopPet.UI;

namespace DesktopPet.Interfaces;

public interface IBehaviourState
{
    bool IsDone { get; }
    void OnStart();
    bool CanTick();
    void Tick();
    void OnEnd();
}