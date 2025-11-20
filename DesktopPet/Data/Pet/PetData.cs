using System.Text.Json.Serialization;
using DesktopPet.Data.Attributes;

namespace DesktopPet.Data.Pet;

public class PetData
{
    public string PetName { get; set; } = "";
    public List<PetAttribute> Attributes { get; set; } = new List<PetAttribute>();
    public bool IsDefault { get; set; } = false;

    public PetData(string petName, bool isDefault = false)
    {
        PetName = petName;
        IsDefault = isDefault;
        Attributes = new List<PetAttribute>();
    }

    [JsonConstructor]
    public PetData(string petName, List<PetAttribute> attributes, bool isDefault = false)
    {
        PetName = petName;
        Attributes = attributes;
        IsDefault = isDefault;
    }

}