using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopPet.Data.Attributes;

public partial class PetAttribute : ObservableObject
{
    [ObservableProperty] private string _name;

    [ObservableProperty] private string _value;

    [JsonConstructor]
    public PetAttribute(string name, string value)
    {
        _name = name;
        _value = value;
    }
}