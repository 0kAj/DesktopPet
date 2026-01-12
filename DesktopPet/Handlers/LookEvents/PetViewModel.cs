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
    private readonly PetBrain _brain;

    private readonly PetEventManager _eventManager;
    [ObservableProperty] private double _eyeScaleX;
    [ObservableProperty] private double _eyeScaleY;

    [ObservableProperty] private double _lookDirectionX;

    [ObservableProperty] private double _lookDirectionY;

    [ObservableProperty] private ContextMenu? _petContextMenu;

    [ObservableProperty] private string _petName;

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

    public PetAttribute PrimaryColorAttribute { get; set; }
    public PetAttribute SecondaryColorAttribute { get; set; }

    public void Init()
    {
        WindowWidth = 150;
        WindowHeight = 100;
        ObjectWidth = 50;
        ObjectHeight = 50;
        Left = -WindowWidth;
        Top = SystemParameters.WorkArea.Bottom - CollisionRect.Height - (CollisionRect.Top - Top);
    }

    protected override void Tick()
    {
        base.Tick();
        _brain.Tick();
    }

    protected override void Tick(float delta)
    {
    }
}