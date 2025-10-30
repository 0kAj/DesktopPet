using System.Windows;
using System.Windows.Input;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States;

public class DragDropMovementState(PetWindow petWindow) : IBehaviourState
{
    private Point _dragStartPos;
    private Vector _dragStartWindowPos;

    public bool IsDone => false; // always draggable
    public void OnStart()
    {
        // MouseDown="Pet_OnMouseDown"
        // MouseUp="Pet_OnMouseUp"
        petWindow.MouseDown += Pet_OnMouseDown;
        petWindow.MouseUp += Pet_OnMouseUp;
    }

    public bool CanTick() => true; // always draggable

    public void Tick()
    {
        if (petWindow.IsOnDragging)
        {
            var dragPos = petWindow.GetDPISaveGlobalMousePos();
            var dir = dragPos - _dragStartPos;
            var targetpos = _dragStartWindowPos + dir;
            petWindow.Left = targetpos.X;
            petWindow.Top = targetpos.Y;
        }
    }

    public void OnEnd()
    {
        
    }
    
    
    private void Pet_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        // drag start
        petWindow.IsOnDragging = true;
        _dragStartPos = petWindow.GetDPISaveGlobalMousePos();
        _dragStartWindowPos = new Vector(petWindow.Left, petWindow.Top);
        petWindow.debugLabel.Content = "Mouse Down";
        petWindow.CaptureMouse();
    }

    private void Pet_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        // drag end
        petWindow.IsOnGround = false;
        petWindow.IsOnDragging = false;
        petWindow.debugLabel.Content = "Mouse Up";
        petWindow.ReleaseMouseCapture();
    }
}