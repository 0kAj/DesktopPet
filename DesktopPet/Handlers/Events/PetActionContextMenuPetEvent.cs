using System.Windows;
using System.Windows.Controls;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.Events;

public class PetActionContextMenuPetEvent : PetEvent
{
    
    private ContextMenu _petActionContextMenu;
    
    private PetWindow _petWindow;

    public PetActionContextMenuPetEvent(PetWindow petWindow)
    {
        _petWindow = petWindow;

        _petWindow.pet.ContextMenu = CreatePetActionContextMenu();
    }

    private ContextMenu CreatePetActionContextMenu()
    {
        var cm =  new ContextMenu();
        
        var playMenuItem = new MenuItem();
        playMenuItem.Header = "Play";
        playMenuItem.Click += PlayMenuItem_OnClick;
        cm.Items.Add(playMenuItem);
        
        var feedMenuItem = new MenuItem();
        feedMenuItem.Header = "Feed";
        feedMenuItem.Click += FeedMenuItem_OnClick;
        cm.Items.Add(feedMenuItem);
        
        cm.Items.Add(new Separator());
        
        var backMenuItem = new MenuItem();
        backMenuItem.Header = "Back";
        backMenuItem.Click += BackMenuItem_OnClick;
        cm.Items.Add(backMenuItem);
        
        return cm;
    }

    private void PlayMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // TODO handle PlayMenuItem_OnClick
        // Var. 1 OpenGameWindow
        // Var. 2 ContextSubMenu with Games
        _petWindow.debugLabel.Content = "Play";
    }
    
    private void FeedMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // Start FeedGame
        _petWindow.debugLabel.Content = "Feed";
    }
    
    private void BackMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        // close ContextMenu
        if (_petWindow.pet.ContextMenu != null) 
            _petWindow.pet.ContextMenu.IsOpen = false;
    }

    public void OnUnregister()
    {
        // Remove PetActionContextMenu
        _petWindow.pet.ContextMenu = null;
    }
}