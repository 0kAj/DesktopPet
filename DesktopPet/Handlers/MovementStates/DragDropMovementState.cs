using System.Windows;
using System.Windows.Input;
using DesktopPet.Interfaces;
using DesktopPet.WPF;
using PetWindow = DesktopPet.WPF.PetWindow;

namespace DesktopPet.Handlers.MovementStates;

public class DragDropMovementState : IBehaviourState
{
    private readonly PetBrain _brain;
    private readonly PetWindow _petWindow;
    private Point _dragStartPos;
    private Vector _dragStartWindowPos;

    private PetEventManager _eventManager;

    public DragDropMovementState(PetBrain petBrain, PetEventManager eventManager)
    {
        _brain = petBrain;
        _eventManager = eventManager;

        _petWindow = petBrain.PetWindow;
        // MouseDown="Pet_OnMouseDown"
        // MouseUp="Pet_OnMouseUp"

        _eventManager.MouseDown += Pet_OnMouseDown;
        _eventManager.MouseUp += Pet_OnMouseUp;
    }

    public bool IsDone => false; // always draggable

    public bool CanTick()
    {
        return true;
        // always draggable
    }

    public void Tick()
    {
        if (_brain.IsOnDragging)
        {
            var dragPos = _petWindow.GetDpiSaveGlobalMousePos();
            var dir = dragPos - _dragStartPos;
            var targetpos = _dragStartWindowPos + dir;
            _petWindow.Left = targetpos.X;
            _petWindow.Top = targetpos.Y;
        }
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
        _eventManager.MouseDown -= Pet_OnMouseDown;
        _eventManager.MouseUp -= Pet_OnMouseUp;
    }


    private void Pet_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        // check if leftclick
        if (e.ChangedButton != MouseButton.Left) return;

        // drag start
        _brain.IsOnDragging = true;
        _brain.IsOnGround = false;
        _petWindow.ResetVelocity();
        _dragStartPos = _petWindow.GetDpiSaveGlobalMousePos();
        _dragStartWindowPos = new Vector(_petWindow.Left, _petWindow.Top);
        // _petWindow.debugLabel.Content = "Mouse Down";
        // _petWindow.CaptureMouse();
        _eventManager.OnCaptureMouse();
    }

    private void Pet_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        // check if leftclick
        if (e.ChangedButton != MouseButton.Left) return;
        // drag end
        _brain.IsOnDragging = false;
        // _petWindow.debugLabel.Content = "Mouse Up";
        // _petWindow.ReleaseMouseCapture();
        _eventManager.OnReleaseMouseCapture();
    }
}