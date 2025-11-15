using DesktopPet.Interfaces;

namespace DesktopPet.Handlers;

public class PetEventHandler
{
    private readonly List<IPetEvent> _activePetEvents = new();

    public PetEventHandler(params IPetEvent[] activePetEvents)
    {
        _activePetEvents.AddRange(activePetEvents);
    }

    public void AddPetEvent(IPetEvent petEvent)
    {
        _activePetEvents.Add(petEvent);
    }

    public void RemovePetEvent(IPetEvent petEvent)
    {
        petEvent.OnUnregister();
        _activePetEvents.Remove(petEvent);
    }

    public void ClearStates()
    {
        var toRemove = _activePetEvents.ToArray(); // copy
        foreach (var petEvent in toRemove)
            RemovePetEvent(petEvent);
    }
}