using System.Windows;
using DesktopPet.Handlers;

namespace DesktopPet.UI;

public partial class PetWindow : VelocityWindow
{
    private readonly PetBrain _brain;

    public PetWindow()
    {
        InitializeComponent();
        // give it a brain
        _brain = new PetBrain(this);
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);
    }

    protected override void Tick()
    {
        base.Tick();
        _brain.Tick();
    }

    //todo add stats to the pet and save/ load them

    public override Rect GetCollisionRect()
    {
        var res = pet.PointToScreen(new Point(0, 0));
        return new Rect(res.X, res.Y, pet.ActualWidth, pet.ActualHeight);
    }
}