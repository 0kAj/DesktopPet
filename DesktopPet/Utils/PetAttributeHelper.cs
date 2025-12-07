using DesktopPet.Data.Attributes;
using DesktopPet.Data.Pet;

namespace DesktopPet.Utils;

public static class PetAttributeHelper
{
    public const string CollectedFoodName = "collectedFood";
    public const string CollectedThirstName = "collectedThirst";
    public const string PetHungerName = "hunger";
    public const string PetThirstName = "thurst";

    public static void InitAttributes(string petName,
        out PetAttribute hunger,
        out PetAttribute thirst,
        out PetAttribute collectedFood,
        out PetAttribute collectedThirst)
    {
        InitStatsAttributes(petName, out hunger, out thirst);
        InitCollectedAttributes(petName, out collectedFood, out collectedThirst);

        // save defaults if required
        PetManager.Instance.SetAttribute(petName, hunger);
        PetManager.Instance.SetAttribute(petName, thirst);
        PetManager.Instance.SetAttribute(petName, collectedFood);
        PetManager.Instance.SetAttribute(petName, collectedThirst);
    }

    public static void InitCollectedAttributes(string petName,
        out PetAttribute collectedFood,
        out PetAttribute collectedThirst)
    {
        var pet = PetManager.Instance.GetPet(petName)!;

        collectedFood = pet.Attributes.FirstOrDefault(a => a.Name == CollectedFoodName)
                        ?? new PetAttribute(CollectedFoodName, "0");

        collectedThirst = pet.Attributes.FirstOrDefault(a => a.Name == CollectedThirstName)
                          ?? new PetAttribute(CollectedThirstName, "0");

        // save defaults if required
        PetManager.Instance.SetAttribute(petName, collectedFood);
        PetManager.Instance.SetAttribute(petName, collectedThirst);
    }

    public static void InitStatsAttributes(string petName,
        out PetAttribute hunger,
        out PetAttribute thirst)
    {
        var pet = PetManager.Instance.GetPet(petName)!;

        hunger = pet.Attributes.FirstOrDefault(a => a.Name == PetHungerName)
                 ?? new PetAttribute(PetHungerName, "100");

        thirst = pet.Attributes.FirstOrDefault(a => a.Name == PetThirstName)
                 ?? new PetAttribute(PetThirstName, "100");

        // save defaults if required
        PetManager.Instance.SetAttribute(petName, hunger);
        PetManager.Instance.SetAttribute(petName, thirst);
    }
}