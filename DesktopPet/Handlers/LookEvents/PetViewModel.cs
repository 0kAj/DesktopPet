using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using DesktopPet.Data.Attributes;
using DesktopPet.Engine.GameObjects;
using DesktopPet.Utils;
using DesktopPet.WPF;

namespace DesktopPet.Handlers.LookEvents;

public partial class PetViewModel : VelocityGameObject
{
    [ObservableProperty] private double _eyeScaleX;
    [ObservableProperty] private double _eyeScaleY;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(BodyShadowAngle))]
    private double _lookDirectionX;

    [ObservableProperty] private double _lookDirectionY;
    
    private readonly PetBrain _brain;
    
    private readonly PetEventManager _eventManager;
    
    [ObservableProperty]
    private string _petName;
    
    [ObservableProperty]
    private ContextMenu _petContextMenu;
    
    public PetViewModel(PetEventManager eventManager, string petName)
    {
        PetName = petName;
        _eventManager = eventManager;
        PetAttributeHelper.InitPetColorAttributes(petName, out var primaryColor, out var secondaryColor);
        PrimaryColorAttribute = primaryColor;
        SecondaryColorAttribute = secondaryColor;
        
        // give it a brain
        _brain = new PetBrain(this) { Name = petName };
        _brain.InitFromMovementTemplate(PetBrain.MovementTemplate.DefaultPet);
        
        _eventManager.Pause += StopTicking;
        _eventManager.Resume += StartTicking;
    }

    public void Init()
    {
        WindowWidth = 150;
        WindowHeight = 100;
        ObjectWidth = 50;
        ObjectHeight = 50;
        Left = -WindowWidth;
        Top = SystemParameters.WorkArea.Bottom - CollisionRect.Height - (CollisionRect.Top - Top);
    }

    public PetAttribute PrimaryColorAttribute { get; set; }
    public PetAttribute SecondaryColorAttribute { get; set; }

    public double BodyShadowAngle => Math.Sign(LookDirectionX) switch
    {
        > 0 => 180,
        0 => 5,
        < 0 => 0
    };

    protected override void Tick()
    {
        base.Tick();
        _brain.Tick();
    }

    protected override void Tick(float delta)
    {
        // debugLabel.Content = delta;
    }
}