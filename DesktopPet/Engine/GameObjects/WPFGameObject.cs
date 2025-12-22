using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopPet.Engine.GameObjects;

public partial class WPFGameObject : ObservableObject //equivalent to engine.Window
{
    [ObservableProperty] private double _left;

    [ObservableProperty] private double _objectHeight;

    [ObservableProperty] private double _objectWidth;

    [ObservableProperty] private double _top;

    [ObservableProperty] private double _windowHeight;

    [ObservableProperty] private double _windowWidth;

    public Vector GetPositionVector()
    {
        return new Vector(Left + WindowWidth / 2, Top);
    }
}