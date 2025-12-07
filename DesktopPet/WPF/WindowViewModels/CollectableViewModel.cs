using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using DesktopPet.MiniGames;

namespace DesktopPet.WPF.WindowViewModels;

public partial class CollectableViewModel : ObservableObject
{
    [ObservableProperty] private double _x;
    [ObservableProperty] private double _y;
    [ObservableProperty] private double _width = 20;
    [ObservableProperty] private double _height = 20;
    [ObservableProperty] private double _speed;
    [ObservableProperty] private Uri? _imageUri;
    [ObservableProperty] private CollectableType _type;

    public Rect CollisionRect => new(X, Y, Width, Height);

    public void Move(double dy) => Y += dy;
}
