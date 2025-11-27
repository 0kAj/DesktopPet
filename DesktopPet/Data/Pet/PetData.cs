using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using DesktopPet.Data.Attributes;

namespace DesktopPet.Data.Pet;

public class PetData : INotifyPropertyChanged
{
    private bool _isDefault;

    private string _petName;

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

    public string PetName
    {
        get => _petName;
        set
        {
            if (_petName != value)
            {
                _petName = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<PetAttribute> Attributes { get; set; }

    public bool IsDefault
    {
        get => _isDefault;
        set
        {
            if (_isDefault != value)
            {
                _isDefault = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<string> LastPlayedGames { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}