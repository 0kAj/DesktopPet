using DesktopPet.Handlers;

namespace DesktopPet.MiniGames;

public abstract class MiniGameWindow : TickingWindow
{
    protected readonly PetBrain _brain;

    protected MiniGameWindow(PetBrain brain)
    {
        _brain = brain;
    }

    public abstract string GameName { get; }
    public abstract void Start();
    public abstract void End();
}