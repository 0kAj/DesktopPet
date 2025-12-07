using System.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopPet.Background;
using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;

namespace DesktopPet.WPF.WindowViewModels;

public partial class WelcomeWindowViewModel : ObservableObject
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsErrorMessageVisible))]
    private string? _errorMessage;

    [ObservableProperty] private string? _petName;

    [ObservableProperty] private bool _setAsDefaultPet = true;

    public bool IsErrorMessageVisible => !string.IsNullOrWhiteSpace(ErrorMessage);
    public event Action? RequestClose;
    public event Action<string>? RequestOpenPetWindow;

    [RelayCommand]
    private void Ok()
    {
        // create new Pet
        if (string.IsNullOrWhiteSpace(PetName))
        {
            ErrorMessage = "Pet name cannot be empty.";
            SystemSounds.Asterisk.Play();
            return;
        }

        PetManager.Instance.SetAttribute(PetName, new PetAttribute("thurst", "100"));
        PetManager.Instance.SetAttribute(PetName, new PetAttribute("hunger", "100"));

        if (SetAsDefaultPet)
            PetManager.Instance.SetDefaultPet(PetName);

        PetStatUpdater.Instance.PetName = PetName;

        RequestOpenPetWindow?.Invoke(PetName);
        RequestClose?.Invoke();
    }

    [RelayCommand]
    private void Close()
    {
        RequestClose?.Invoke();
    }
}