using System.Windows;
using DesktopPet.Data.Pet;
using DesktopPet.Engine;
using DesktopPet.Handlers;

namespace DesktopPet.MiniGames;

public class GameManager
{
    private GameManager()
    {
    }

    public static GameManager Instance { get; } = new();

    public void StartGame(string name, PetBrain petBrain)
    {
        var game = MiniGameRegistry.Create(name, petBrain);
        if (game == null)
        {
            MessageBox.Show($"Game not found: {name}");
            return;
        }

        game.Show();
        game.Start();
        PetManager.Instance.SetLastPlayedGame(petBrain.Name, game.GameName);
    }

    public MiniGameWindow? GetMiniGameByName(string name, PetBrain brain)
    {
        return MiniGameRegistry.Create(name, brain);
    }

    public IEnumerable<string> GetRegisteredGames()
    {
        return MiniGameRegistry.GameNames;
    }
}