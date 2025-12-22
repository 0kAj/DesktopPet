using System.Windows;
using System.Windows.Controls;
using DesktopPet.Data.Pet;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;
using DesktopPet.MiniGames;
using FontAwesome.WPF;
using Microsoft.Extensions.DependencyInjection;
using GameSelectorWindow = DesktopPet.WPF.GameSelectorWindow;
using PetWindow = DesktopPet.WPF.PetWindow;

namespace DesktopPet.Handlers.Events;

public class PetActionContextMenuPetEvent : IPetEvent
{
    private readonly PetBrain _brain;
    private readonly PetViewModel _petViewModel;

    public PetActionContextMenuPetEvent(PetBrain petBrain)
    {
        _brain = petBrain;
        _petViewModel = petBrain.PetViewModel;

        _petViewModel.PetContextMenu = CreatePetActionContextMenu();
    }

    public void OnUnregister()
    {
        // Remove PetActionContextMenu
        _petViewModel.PetContextMenu = null;
    }

    private ContextMenu CreatePetActionContextMenu() //todo as Binding?
    {
        var gameManager = App.Host.Services.GetRequiredService<GameManager>();
        
        var cm = new ContextMenu();


        var gameSelectorMenuItem = new MenuItem();
        gameSelectorMenuItem.Header = "Game Selector"; // Game Selector
        gameSelectorMenuItem.Icon = 
        gameSelectorMenuItem.Icon = CreateIcon(FontAwesomeIcon.Gamepad);
        // gameSelectorMenuItem.Click += (_, _) => App.Host.Services.GetRequiredService<GameSelectorWindow>().Show();

        foreach (var game in gameManager.GetRegisteredGames())
        {
            var gameTypeMenuItem = new MenuItem();
            gameTypeMenuItem.Header = game;
            gameTypeMenuItem.Icon = CreateIcon(FontAwesomeIcon.Gamepad);
            gameTypeMenuItem.Click += (_, _) => gameManager.StartGame(game, _brain);
            gameSelectorMenuItem.Items.Add(gameTypeMenuItem);
        }

        // add last played Games if required
        var lastPlayedGames = PetManager.Instance.GetLastPlayedGames(_brain.Name);
        if (lastPlayedGames.Count > 0)
        {
            gameSelectorMenuItem.Items.Add(new Separator()); // ------------
            var recentGamesMenuItem = new MenuItem();
            recentGamesMenuItem.Header = "Recent Games"; // Recent Games
            recentGamesMenuItem.Icon = CreateIcon(FontAwesomeIcon.ClockOutline);

            foreach (var game in lastPlayedGames)
            {
                var recentGameMenuItem = new MenuItem();
                recentGameMenuItem.Header = game;
                recentGameMenuItem.Icon = CreateIcon(FontAwesomeIcon.Gamepad);
                recentGameMenuItem.Click += (_, _) => gameManager.StartGame(game, _brain);
                recentGamesMenuItem.Items.Add(recentGameMenuItem);
            }

            gameSelectorMenuItem.Items.Add(recentGamesMenuItem);
        }

        cm.Items.Add(gameSelectorMenuItem);

        cm.Items.Add(new Separator()); // -----------------

        var closeMenuItem = new MenuItem();
        closeMenuItem.Header = "Close DesktopPet";
        closeMenuItem.Icon = CreateIcon(FontAwesomeIcon.Close);
        closeMenuItem.Click += (_, _) => Application.Current.Shutdown();
        cm.Items.Add(closeMenuItem);

        return cm;
    }

    private FontAwesome.WPF.FontAwesome CreateIcon(FontAwesomeIcon icon)
    {
        return new FontAwesome.WPF.FontAwesome
        {
            Icon = icon,
            Width = 16,
            Height = 16
        };
    }
}