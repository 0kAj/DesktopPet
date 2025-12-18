using System.ComponentModel.DataAnnotations;
using DesktopPet.Data.Pet;

namespace DesktopPet.WPF.Validation;

public class UniquePetName : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string petName || string.IsNullOrWhiteSpace(petName))
            return ValidationResult.Success;
        return PetManager.Instance.GetPet(petName) != null
            ? new ValidationResult($"Pet name '{petName}' already exists.")
            :  ValidationResult.Success;
    }
}