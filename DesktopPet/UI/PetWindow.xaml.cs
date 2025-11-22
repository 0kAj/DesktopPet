using System.Windows;
using DesktopPet.Handlers;

namespace DesktopPet.UI;

public partial class PetWindow : VelocityWindow
{
    private readonly PetBrain _brain;

    public PetWindow(string petName)
    {
        InitializeComponent();
        // give it a brain
        _brain = new PetBrain(this) { Name = petName };
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);

        debugLabel.Content = _brain.Name;
        
        StartTicking();
    }

    protected override void Tick()
    {
        base.Tick();
        _brain.Tick();
    }

    public override Rect GetCollisionRect()
    {
        var res = pet.PointToScreen(new Point(0, 0));
        return new Rect(res.X, res.Y, pet.ActualWidth, pet.ActualHeight);
    }
}