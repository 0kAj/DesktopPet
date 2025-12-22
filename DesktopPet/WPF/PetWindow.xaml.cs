using System.Windows;
using DesktopPet.Handlers.LookEvents;

namespace DesktopPet.WPF;

public partial class PetWindow : Window
{
    private readonly PetEventManager _eventManager;
    private readonly PetViewModel _vm;

    public PetWindow(PetEventManager eventManager, string petName)
    {
        InitializeComponent();
        _eventManager = eventManager;

        _vm = new PetViewModel(_eventManager, petName);
        DataContext = _vm;

        ContentRendered += (_, _) => InitViewModel();
        KeyDown += _eventManager.OnKeyDown;
        MouseDown += _eventManager.OnMouseDown;
        MouseUp += _eventManager.OnMouseUp;
        _eventManager.CaptureMouse += () => CaptureMouse();
        _eventManager.ReleaseMouseCapture += ReleaseMouseCapture;
    }

    private void InitViewModel()
    {
        _vm.GetCollisionRect = GetCollisionRect;
        _vm.GetCollisionPositionVector = GetCollisionPositionVector;
        _vm.Init();
        _vm.StartTicking();
    }

    private Rect GetCollisionRect()
    {
        var res = Pet.PointToScreen(new Point(0, 0));
        return new Rect(res.X, res.Y, Pet.ActualWidth, Pet.ActualHeight);
    }

    private Vector GetCollisionPositionVector()
    {
        var res = Pet.PointToScreen(new Point(0, 0));
        return new Vector(res.X + Pet.ActualWidth / 2, res.Y);
    }
}