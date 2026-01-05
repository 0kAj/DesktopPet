using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopPet.Background;
using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;
using DesktopPet.WPF.Validation;

namespace DesktopPet.WPF.WindowViewModels;

public partial class WelcomeWindowViewModel : ObservableValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(AllowEmptyStrings = false, ErrorMessage = "pet name is required.")]
    [MinLength(3, ErrorMessage = "pet name must be at least 3 characters long.")]
    [UniquePetName]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string? _petName;

    [ObservableProperty] private bool _setAsDefaultPet = true;

    public WelcomeWindowViewModel()
    {
        ValidateAllProperties();
    }

    private bool HasNoErrors => !HasErrors;

    public event Action? RequestClose;
    public event Action<string>? RequestOpenPetWindow;

    [RelayCommand(CanExecute = nameof(HasNoErrors))]
    private void Ok()
    {
        //remove whitespace at the end and the beginning of the petname
        PetName = PetName!.Trim();
        
        // create new Pet
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