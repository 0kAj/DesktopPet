using DesktopPet.Interfaces;

namespace DesktopPet.Handlers;

public class PetMovementHandler
{
    private List<IBehaviourState> _behaviourStates = new();

    public PetMovementHandler(params IBehaviourState[] startStates)
    {
        _behaviourStates.AddRange(startStates);
    }

    public void Tick()
    {
        // Alle triggerbaren States gleichzeitig aktivieren
        foreach (var state in _behaviourStates.Where(s => s.CanTick()))
        {
            state.Tick();
            if (state.IsDone)
                state.OnEnd();
        }
    }

    public void AddState(IBehaviourState state)
    {
        _behaviourStates.Add(state);
    }

    public void RemoveState(IBehaviourState state)
    {
        _behaviourStates.Remove(state);
    }
}