using System.Windows;
using System.Windows.Controls;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.Events;

public class PetActionContextMenuPetEvent : IPetEvent
{
    private readonly PetWindow _petWindow;
    private readonly PetBrain _brain;

    public PetActionContextMenuPetEvent(PetBrain petBrain)
    {
        _brain = petBrain;
        _petWindow = petBrain.PetWindow;

        _petWindow.pet.ContextMenu = CreatePetActionContextMenu();
    }

    public void OnUnregister()
    {
        // Remove PetActionContextMenu
        _petWindow.pet.ContextMenu = null;
    }

    private ContextMenu CreatePetActionContextMenu()
    {
        var cm = new ContextMenu();

        var playMenuItem = new MenuItem();
        playMenuItem.Header = "Play";
        var gameSelectorMenuItem = new MenuItem();
        gameSelectorMenuItem.Header = "Game Selector"; // Game Selector
        gameSelectorMenuItem.Click += (_, _) => new GameSelectorWindow(_brain).Show();
        playMenuItem.Items.Add(gameSelectorMenuItem);
        playMenuItem.Items.Add(new Separator()); // ------------

        foreach (var type in Enum.GetValues(typeof(GameSelectorWindow.GameType)).Cast<GameSelectorWindow.GameType>().ToList())
        {
            var gameTypeMenuItem = new MenuItem();
            gameTypeMenuItem.Header = type.ToString();
            gameTypeMenuItem.Click += (_, _) => GameSelectorWindow.StartGame(type, _brain);
            playMenuItem.Items.Add(gameTypeMenuItem);
        }
        
        playMenuItem.Items.Add(new Separator()); // ------------
        var recentGamesMenuItem = new MenuItem();
        recentGamesMenuItem.Header = "Recent Games"; // Recent Games
        playMenuItem.Items.Add(recentGamesMenuItem);
        cm.Items.Add(playMenuItem);

        var feedMenuItem = new MenuItem();
        feedMenuItem.Header = "Feed";
        feedMenuItem.Click += (_, _) => GameSelectorWindow.StartGame(GameSelectorWindow.GameType.FoodCollector, _brain);
        cm.Items.Add(feedMenuItem);
        cm.Items.Add(new Separator());

        var backMenuItem = new MenuItem();
        backMenuItem.Header = "Back";
        backMenuItem.Click += (_, _) => _petWindow.pet.ContextMenu!.IsOpen = false;
        cm.Items.Add(backMenuItem);

        return cm;
    }
}