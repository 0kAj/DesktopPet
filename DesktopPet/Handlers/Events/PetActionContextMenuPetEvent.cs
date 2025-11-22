using System.Windows;
using System.Windows.Controls;
using DesktopPet.Data.Pet;
using DesktopPet.Interfaces;
using DesktopPet.MiniGames;
using DesktopPet.UI;

namespace DesktopPet.Handlers.Events;

public class PetActionContextMenuPetEvent : IPetEvent
{
    private readonly PetBrain _brain;
    private readonly PetWindow _petWindow;

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

        foreach (var game in GameManager.Instance.GetRegisteredGames())
        {
            var gameTypeMenuItem = new MenuItem();
            gameTypeMenuItem.Header = game;
            gameTypeMenuItem.Click += (_, _) => GameManager.Instance.StartGame(game, _brain);
            playMenuItem.Items.Add(gameTypeMenuItem);
        }

        playMenuItem.Items.Add(new Separator()); // ------------
        var recentGamesMenuItem = new MenuItem();
        recentGamesMenuItem.Header = "Recent Games"; // Recent Games

        foreach (var game in PetManager.Instance.GetLastPlayedGames(_brain.Name))
        {
            var recentGameMenuItem = new MenuItem();
            recentGameMenuItem.Header = game;
            recentGameMenuItem.Click += (_, _) => GameManager.Instance.StartGame(game, _brain);
            recentGamesMenuItem.Items.Add(recentGameMenuItem);
        }

        playMenuItem.Items.Add(recentGamesMenuItem);
        cm.Items.Add(playMenuItem);

        var feedMenuItem = new MenuItem();
        feedMenuItem.Header = "Feed";
        feedMenuItem.Click += (_, _) => GameManager.Instance.StartGame("Food Collector", _brain);
        cm.Items.Add(feedMenuItem);
        cm.Items.Add(new Separator());

        var backMenuItem = new MenuItem();
        backMenuItem.Header = "Back";
        backMenuItem.Click += (_, _) => _petWindow.pet.ContextMenu!.IsOpen = false;
        cm.Items.Add(backMenuItem);
        
        cm.Items.Add(new Separator()); // -----------------
        
        var closeMenuItem = new MenuItem();
        closeMenuItem.Header = "Close";
        closeMenuItem.Click += (_, _) => Application.Current.Shutdown();
        cm.Items.Add(closeMenuItem);

        return cm;
    }
}