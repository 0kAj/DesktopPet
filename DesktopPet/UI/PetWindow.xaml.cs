using System.Windows;
using System.Windows.Input;
using DesktopPet.Movement;
using DesktopPet.Movement.States;

namespace DesktopPet.UI;

public partial class PetWindow : TickingWindow
{
    private readonly PetMovementHandler _petMovementHandler;
    
    public bool IsOnGround { get; set; }
    
    public PetWindow()
    {
        InitializeComponent();
        _petMovementHandler = new PetMovementHandler(
            new GravityMovementState(this),
            new MovingMovementState(this));
    }

    protected override void Tick(double deltaTime)
    {
        // lasse das Pet vom Zentrum auf die Taskleiste fallen
        // bewege nach irgendwo auf der Taskleiste
        // bewege es nach rechts
        _petMovementHandler.Tick(deltaTime);
    }
}