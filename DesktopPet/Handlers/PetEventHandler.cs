using DesktopPet.Interfaces;

namespace DesktopPet.Handlers;

public class PetEventHandler
{
    private List<PetEvent> _activePetEvents = new ();

    public PetEventHandler(params PetEvent[] activePetEvents)
    {
        _activePetEvents.AddRange(activePetEvents);
    }

    public void AddPetEvent(PetEvent petEvent)
    {
        _activePetEvents.Add(petEvent);
    }

    public void RemovePetEvent(PetEvent petEvent)
    {
        petEvent.OnUnregister();
        _activePetEvents.Remove(petEvent);
    }
}