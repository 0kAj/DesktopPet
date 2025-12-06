using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using DesktopPet.Data.Attributes;

namespace DesktopPet.Data.Pet;

public partial class PetData : ObservableObject
{
    [ObservableProperty] private bool _isDefault;

    [ObservableProperty] private string _petName;

    public PetData(string petName, bool isDefault = false)
    {
        PetName = petName;
        IsDefault = isDefault;
        Attributes = new ObservableCollection<PetAttribute>();
        LastPlayedGames = new ObservableCollection<string>();
    }

    [JsonConstructor]
    public PetData(string petName, ObservableCollection<PetAttribute> attributes,
        ObservableCollection<string> lastPlayedGames, bool isDefault = false)
    {
        PetName = petName;
        Attributes = new ObservableCollection<PetAttribute>(attributes);
        IsDefault = isDefault;
        LastPlayedGames = new ObservableCollection<string>(lastPlayedGames);
    }

    public ObservableCollection<PetAttribute> Attributes { get; set; }

    public ObservableCollection<string> LastPlayedGames { get; set; }
}