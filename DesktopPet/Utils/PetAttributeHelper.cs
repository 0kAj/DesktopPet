using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;
using DesktopPet.Handlers;

namespace DesktopPet.Utils;

public static class PetAttributeHelper
{
    public const string CollectedFoodName = "collectedFood";
    public const string CollectedThirstName = "collectedThirst";
    public const string PetHungerName = "hunger";
    public const string PetThirstName = "thurst";

    public static void InitAttributes(PetBrain brain,
        out PetAttribute hunger,
        out PetAttribute thirst,
        out PetAttribute collectedFood,
        out PetAttribute collectedThirst)
    {
        var pet = PetManager.Instance.GetPet(brain.Name);

        hunger = pet!.Attributes.FirstOrDefault(a => a.Name == PetHungerName)
                 ?? new PetAttribute(PetHungerName, "100");

        thirst = pet.Attributes.FirstOrDefault(a => a.Name == PetThirstName)
                 ?? new PetAttribute(PetThirstName, "100");

        collectedFood = pet.Attributes.FirstOrDefault(a => a.Name == CollectedFoodName)
                        ?? new PetAttribute(CollectedFoodName, "0");

        collectedThirst = pet.Attributes.FirstOrDefault(a => a.Name == CollectedThirstName)
                          ?? new PetAttribute(CollectedThirstName, "0");

        // save defaults if required
        PetManager.Instance.SetAttribute(brain.Name, hunger);
        PetManager.Instance.SetAttribute(brain.Name, thirst);
        PetManager.Instance.SetAttribute(brain.Name, collectedFood);
        PetManager.Instance.SetAttribute(brain.Name, collectedThirst);
    }

    public static void InitAttributes(PetBrain brain,
        out PetAttribute collectedFood,
        out PetAttribute collectedThirst)
    {
        var pet = PetManager.Instance.GetPet(brain.Name);

        collectedFood = pet!.Attributes.FirstOrDefault(a => a.Name == CollectedFoodName)
                        ?? new PetAttribute(CollectedFoodName, "0");

        collectedThirst = pet.Attributes.FirstOrDefault(a => a.Name == CollectedThirstName)
                          ?? new PetAttribute(CollectedThirstName, "0");

        // save defaults if required
        PetManager.Instance.SetAttribute(brain.Name, collectedFood);
        PetManager.Instance.SetAttribute(brain.Name, collectedThirst);
    }
}