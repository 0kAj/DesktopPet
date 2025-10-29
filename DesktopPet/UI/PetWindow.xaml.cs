using System.Windows;
using System.Windows.Input;
using DesktopPet.Movement;
using DesktopPet.Movement.States;

namespace DesktopPet.UI;

public partial class PetWindow : TickingWindow
{
    private readonly PetMovementHandler _petMovementHandler;
    
    public bool IsOnGround { get; set; }
    
    public bool IsOnDragging { get; set; }

    private Point dragStartPos;
    private Vector dragStartWindowPos;
    
    public PetWindow()
    {
        InitializeComponent();
        _petMovementHandler = new PetMovementHandler(
            new GravityMovementState(this));
            // new MovingMovementState(this)
    }

    protected override void Tick()
    {
        // lasse das Pet vom Zentrum auf die Taskleiste fallen
        // bewege nach irgendwo auf der Taskleiste
        // bewege es nach rechts
        _petMovementHandler.Tick();

        if (IsOnDragging)
        {
            var dragPos = GetDPISaveGlobalMousePos();
            var dir = dragPos - dragStartPos;
            var targetpos = dragStartWindowPos + dir;
            Left = targetpos.X;
            Top = targetpos.Y;
        }
    }

    private void Pet_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        // drag start
        IsOnDragging = true;
        dragStartPos = GetDPISaveGlobalMousePos();
        dragStartWindowPos = new Vector(Left, Top);
        debugLabel.Content = "Mouse Down";
        CaptureMouse();
    }

    private void Pet_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        // drag end
        IsOnGround = false;
        IsOnDragging = false;
        debugLabel.Content = "Mouse Up";
        ReleaseMouseCapture();
    }
}