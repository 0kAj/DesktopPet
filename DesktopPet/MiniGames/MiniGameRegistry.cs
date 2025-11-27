using System.Reflection;
using DesktopPet.Attribute;
using DesktopPet.Engine;
using DesktopPet.Handlers;

namespace DesktopPet.MiniGames;

public static class MiniGameRegistry
{
    private static readonly Dictionary<string, Func<PetBrain, MiniGameWindow>> Games = new();

    // be static to get called when the Application starts
    static MiniGameRegistry()
    {
        RegisterAllGamesFromAssembly();
    }

    public static IEnumerable<string> GameNames => Games.Keys;

    public static void Register(string name, Func<PetBrain, MiniGameWindow> creator)
    {
        Games[name] = creator;
    }

    public static MiniGameWindow? Create(string name, PetBrain brain)
    {
        if (!Games.ContainsKey(name)) return null;
        var game = Games[name];
        return game(brain);
    }

    private static void RegisterAllGamesFromAssembly()
    {
        var typesInAssembly = Assembly.GetExecutingAssembly().GetTypes();

        foreach (var type in typesInAssembly)
            // instantiable MiniGameWindow && has custom Attribute [MiniGame("buijdfgwu")]
            if (type.IsAssignableTo(typeof(MiniGameWindow))
                && type.GetCustomAttributes(typeof(MiniGameAttribute), true).Any())
            {
                // register minigame to Assembly
                // constructor minigame(petbrain)
                var gameConstructor = type.GetConstructor(new[] { typeof(PetBrain) });

                // Game name from attribute MiniGame["dgtuguohjb"]
                var gameName = type.GetCustomAttribute<MiniGameAttribute>()!.GameName;

                // register gamename  &&  save costructor(petBrain) with petBrain as func param
                Register(gameName, petBrain => (MiniGameWindow)gameConstructor!.Invoke([petBrain]));
            }
    }
}