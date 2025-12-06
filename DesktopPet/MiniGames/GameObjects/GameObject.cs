using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopPet.MiniGames.GameObjects;

public partial class GameObject : ObservableObject
{
    [ObservableProperty] private double _x;

    [ObservableProperty] private double _y;
}