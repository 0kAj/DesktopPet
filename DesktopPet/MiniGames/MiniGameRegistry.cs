using System.Reflection;
using DesktopPet.Attribute;
using DesktopPet.Engine;
using DesktopPet.Handlers;
using DesktopPet.WPF;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopPet.MiniGames;

public static class MiniGameRegistry
{
    private static readonly Dictionary<string, Func<PetBrain, PetEventManager, MiniGameWindow>> Games = new();

    private static readonly PetEventManager _eventManager;

    // be static to get called when the Application starts
    static MiniGameRegistry()
    {
        _eventManager = App.Host.Services.GetRequiredService<PetEventManager>();
        RegisterAllGamesFromAssembly();
    }

    public static IEnumerable<string> GameNames => Games.Keys;

    public static void Register(string name, Func<PetBrain, PetEventManager, MiniGameWindow> creator)
    {
        Games[name] = creator;
    }

    public static MiniGameWindow? Create(string name, PetBrain brain)
    {
        if (!Games.ContainsKey(name)) return null;
        var game = Games[name];
        return game(brain, _eventManager);
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
                var gameConstructor = type.GetConstructor(new[] { typeof(PetBrain), typeof(PetEventManager) });

                // Game name from attribute MiniGame["dgtuguohjb"]
                var gameName = type.GetCustomAttribute<MiniGameAttribute>()!.GameName;

                // register gamename  &&  save costructor(petBrain) with petBrain as func param
                Register(gameName,
                    (petBrain, eventManager) => (MiniGameWindow)gameConstructor!.Invoke([petBrain, eventManager]));
            }
    }
}