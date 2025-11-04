using System.Windows;
using System.Windows.Input;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Handlers.MovementStates;

public class DragDropMovementState : IBehaviourState
{
    private readonly PetWindow _petWindow;
    private Point _dragStartPos;
    private Vector _dragStartWindowPos;

    public DragDropMovementState(PetWindow petWindow)
    {
        _petWindow = petWindow;
        // MouseDown="Pet_OnMouseDown"
        // MouseUp="Pet_OnMouseUp"
        petWindow.MouseDown += Pet_OnMouseDown;
        petWindow.MouseUp += Pet_OnMouseUp;
    }

    public bool IsDone => false; // always draggable

    public bool CanTick()
    {
        return true;
        // always draggable
    }

    public void Tick()
    {
        if (_petWindow.IsOnDragging)
        {
            var dragPos = _petWindow.GetDPISaveGlobalMousePos();
            var dir = dragPos - _dragStartPos;
            var targetpos = _dragStartWindowPos + dir;
            _petWindow.Left = targetpos.X;
            _petWindow.Top = targetpos.Y;
        }
    }

    public void OnEnd()
    {
    }


    private void Pet_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        // check if leftclick
        if (e.ChangedButton != MouseButton.Left) return;

        // drag start
        _petWindow.IsOnDragging = true;
        _petWindow.IsOnGround = false;
        _petWindow.ResetVelocity();
        _dragStartPos = _petWindow.GetDPISaveGlobalMousePos();
        _dragStartWindowPos = new Vector(_petWindow.Left, _petWindow.Top);
        _petWindow.debugLabel.Content = "Mouse Down";
        _petWindow.CaptureMouse();
    }

    private void Pet_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        // check if leftclick
        if (e.ChangedButton != MouseButton.Left) return;
        // drag end
        _petWindow.IsOnDragging = false;
        _petWindow.debugLabel.Content = "Mouse Up";
        _petWindow.ReleaseMouseCapture();
    }
}