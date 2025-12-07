using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using DesktopPet.Data.Attributes;

namespace DesktopPet.Data.Pet;

public partial class PetData : ObservableObject
{
    [ObservableProperty] private bool _isDefault;

    [ObservableProperty] private string _petName;
    
    public event Action? DataChanged;

    public PetData(string petName, bool isDefault = false)
    {
        PetName = petName;
        IsDefault = isDefault;
        Attributes = new ObservableCollection<PetAttribute>();
        LastPlayedGames = new ObservableCollection<string>();

        SubscribeToPropertyChanged();
    }
    
    private void SubscribeToPropertyChanged()
    {
        PropertyChanged += (_, _) => DataChanged?.Invoke();

        LastPlayedGames.CollectionChanged += (_, _) => DataChanged?.Invoke();

        // Initial subscribe for existing attributes
        foreach (var a in Attributes)
            a.PropertyChanged += AttributeChanged;
    }

    private void AttributesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (PetAttribute item in e.NewItems)
                item.PropertyChanged += AttributeChanged;

        if (e.OldItems != null)
            foreach (PetAttribute item in e.OldItems)
                item.PropertyChanged -= AttributeChanged;

        DataChanged?.Invoke();
    }
    
    private void AttributeChanged(object? sender, PropertyChangedEventArgs e)
    {
        DataChanged?.Invoke();
    }

    [JsonConstructor]
    public PetData(string petName, ObservableCollection<PetAttribute> attributes,
        ObservableCollection<string> lastPlayedGames, bool isDefault = false)
    {
        PetName = petName;
        Attributes = new ObservableCollection<PetAttribute>(attributes);
        IsDefault = isDefault;
        LastPlayedGames = new ObservableCollection<string>(lastPlayedGames);

        SubscribeToPropertyChanged();
    }

    public ObservableCollection<PetAttribute> Attributes { get; set; }

    public ObservableCollection<string> LastPlayedGames { get; set; }
}