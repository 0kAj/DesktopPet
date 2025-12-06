using System.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;

namespace DesktopPet.WPF.WindowViewModels;

public partial class WelcomeWindowViewModel: ObservableObject
{
    public event Action? RequestClose;
    public event Action<string>? RequestOpenPetWindow;
    
    [ObservableProperty]
    private string? _petName;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsErrorMessageVisible))]
    private string? _errorMessage;
    
    public bool IsErrorMessageVisible => !string.IsNullOrWhiteSpace(ErrorMessage);
    
    [ObservableProperty]
    private bool _setAsDefaultPet = true;
    
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

        RequestOpenPetWindow?.Invoke(PetName);
        RequestClose?.Invoke();
    }
    
    [RelayCommand]
    private void Close() => RequestClose?.Invoke();
}