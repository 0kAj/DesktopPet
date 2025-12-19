using CommunityToolkit.Mvvm.ComponentModel;
using DesktopPet.Data.Attributes;
using DesktopPet.Interfaces.Window;
using DesktopPet.Utils;

namespace DesktopPet.Handlers.LookEvents;

public partial class LookingPetViewModel : ObservableObject
{
    private readonly IVelocity _velocity;

    [ObservableProperty] private double _eyeScaleX;
    [ObservableProperty] private double _eyeScaleY;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(BodyShadowAngle))]
    private double _lookDirectionX;

    [ObservableProperty] private double _lookDirectionY;

    public LookingPetViewModel(IVelocity velocity, string petName)
    {
        _velocity = velocity;
        PetAttributeHelper.InitPetColorAttributes(petName, out var primaryColor, out var secondaryColor);
        PrimaryColorAttribute = primaryColor;
        SecondaryColorAttribute = secondaryColor;
    }

    public double VelocityX => _velocity.VelocityX;
    public double VelocityY => _velocity.VelocityY;

    public PetAttribute PrimaryColorAttribute { get; set; }
    public PetAttribute SecondaryColorAttribute { get; set; }

    public double BodyShadowAngle => Math.Sign(LookDirectionX) switch
    {
        > 0 => 180,
        0 => 5,
        < 0 => 0
    };
}