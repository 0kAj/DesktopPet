using System.Text.Json.Serialization;
using DesktopPet.Data.Attributes;

namespace DesktopPet.Data.Pet;

public class PetData
{
    public string PetName { get; set; } = "";
    public List<PetAttribute> Attributes { get; set; } = new List<PetAttribute>();
    public bool IsDefault { get; set; } = false;

    public List<string> LastPlayedGames { get; set; } = new List<string>();

    public PetData(string petName, bool isDefault = false)
    {
        PetName = petName;
        IsDefault = isDefault;
        Attributes = new List<PetAttribute>();
        LastPlayedGames = new List<string>();
    }

    [JsonConstructor]
    public PetData(string petName, List<PetAttribute> attributes, List<string> lastPlayedGames, bool isDefault = false)
    {
        PetName = petName;
        Attributes = attributes;
        IsDefault = isDefault;
        LastPlayedGames = lastPlayedGames;
    }

}