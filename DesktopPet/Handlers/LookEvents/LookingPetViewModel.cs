using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopPet.Handlers.LookEvents;

public partial class LookingPetViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BodyShadowAngle))]
    private double _lookDirectionX;
    [ObservableProperty] private double _lookDirectionY;

    [ObservableProperty] private double _eyeScaleX;
    [ObservableProperty] private double _eyeScaleY;

    private readonly IVelocity _velocity;

    public double VelocityX => _velocity.VelocityX;
    public double VelocityY => _velocity.VelocityY;

    public double BodyShadowAngle => Math.Sign(LookDirectionX) switch
    {
        > 0 => 180,
        0 => 5,
        < 0 => 0
    };

    public LookingPetViewModel(IVelocity velocity)
    {
        _velocity = velocity;
    }
}