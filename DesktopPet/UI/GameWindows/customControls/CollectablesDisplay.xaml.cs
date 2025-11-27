using System.Windows;
using System.Windows.Controls;

namespace DesktopPet.UI.GameWindows.customControls;

public partial class CollectablesDisplay : UserControl
{
    public int Food
    {
        get => (int)GetValue(FoodProperty);
        set => SetValue(FoodProperty, value);
    }

    public static readonly DependencyProperty FoodProperty = // for binding compatibility
        DependencyProperty.Register(
            nameof(Food),
            typeof(int),
            typeof(CollectablesDisplay),
            new PropertyMetadata(0));

    public int Thirst
    {
        get => (int)GetValue(ThirstProperty);
        set => SetValue(ThirstProperty, value);
    }

    public static readonly DependencyProperty ThirstProperty = // for binding compatibility
        DependencyProperty.Register(
            nameof(Thirst),
            typeof(int),
            typeof(CollectablesDisplay),
            new PropertyMetadata(0));


    public int Size
    {
        get => (int)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public static readonly DependencyProperty SizeProperty = // for binding compatibility
        DependencyProperty.Register(
            nameof(Size),
            typeof(int),
            typeof(CollectablesDisplay),
            new PropertyMetadata(50));

    public CollectablesDisplay()
    {
        InitializeComponent();
    }
}