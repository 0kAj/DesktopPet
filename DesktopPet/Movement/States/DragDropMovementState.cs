using System.Windows;
using System.Windows.Input;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class DragDropMovementState : IBehaviourState
{
    private readonly PetWindow _petWindow;
    
    private Point dragStartPos;
    private Vector dragStartWindowPos;

    public DragDropMovementState(PetWindow petWindow)
    {
        _petWindow = petWindow;
    }

    public bool IsDone => false; // always draggable
    public void OnStart()
    {
        // MouseDown="Pet_OnMouseDown"
        // MouseUp="Pet_OnMouseUp"
        _petWindow.MouseDown += Pet_OnMouseDown;
        _petWindow.MouseUp += Pet_OnMouseUp;
    }

    public bool CanTick() => true; // always draggable

    public void Tick()
    {
        if (_petWindow.IsOnDragging)
        {
            var dragPos = _petWindow.GetDPISaveGlobalMousePos();
            var dir = dragPos - dragStartPos;
            var targetpos = dragStartWindowPos + dir;
            _petWindow.Left = targetpos.X;
            _petWindow.Top = targetpos.Y;
        }
    }

    public void OnEnd()
    {
        
    }
    
    
    private void Pet_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        // drag start
        _petWindow.IsOnDragging = true;
        dragStartPos = _petWindow.GetDPISaveGlobalMousePos();
        dragStartWindowPos = new Vector(_petWindow.Left, _petWindow.Top);
        _petWindow.debugLabel.Content = "Mouse Down";
        _petWindow.CaptureMouse();
    }

    private void Pet_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        // drag end
        _petWindow.IsOnGround = false;
        _petWindow.IsOnDragging = false;
        _petWindow.debugLabel.Content = "Mouse Up";
        _petWindow.ReleaseMouseCapture();
    }
}