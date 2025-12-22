using System.Windows;
using System.Windows.Input;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;
using DesktopPet.Utils;
using DesktopPet.WPF;

namespace DesktopPet.Handlers.MovementStates;

public class DragDropMovementState : IBehaviourState
{
    private readonly PetBrain _brain;
    private readonly PetViewModel _petViewModel;
    private Point _dragStartPos;
    private Vector _dragStartWindowPos;

    private PetEventManager _eventManager;

    public DragDropMovementState(PetBrain petBrain, PetEventManager eventManager)
    {
        _brain = petBrain;
        _eventManager = eventManager;

        _petViewModel = petBrain.PetViewModel;
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
            // var dragPos = _petViewModel.GetDpiSaveGlobalMousePos();
            var dragPos = Helper.GetMousePosition();
            var dir = dragPos - _dragStartPos;
            var targetpos = _dragStartWindowPos + dir;
            _petViewModel.Left = targetpos.X;
            _petViewModel.Top = targetpos.Y;
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
        _petViewModel.ResetVelocity();
        // _dragStartPos = _petViewModel.GetDpiSaveGlobalMousePos();
        _dragStartPos = Helper.GetMousePosition();
        _dragStartWindowPos = new Vector(_petViewModel.Left, _petViewModel.Top);
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