using System.Collections.ObjectModel;
using DesktopPet.Data.Attributes;

namespace DesktopPet.Data.Pet;

public class PetManager
{
    public static PetManager Instance = new();
    private readonly Dictionary<string, PetData> _petCache = new();

    private readonly PetStorage _petStorage;

    public PetManager()
    {
        _petStorage = DataManager.Instance.GetData<PetStorage>();
        LoadFromStorage();
    }

    private void LoadFromStorage()
    {
        _petCache.Clear();

        foreach (var pet in _petStorage.Pets)
            _petCache[pet.PetName] = new PetData(
                pet.PetName,
                new ObservableCollection<PetAttribute>(pet.Attributes),
                new ObservableCollection<string>(pet.LastPlayedGames),
                pet.IsDefault
            );
    }

    private void SaveToStorage()
    {
        // copy cache to storage
        _petStorage.Pets = _petCache.Values
            .Select(p => new PetData(
                p.PetName,
                new ObservableCollection<PetAttribute>(p.Attributes),
                new ObservableCollection<string>(p.LastPlayedGames),
                p.IsDefault
            ))
            .ToList();
        DataManager.Instance.SaveData(_petStorage);
    }

    public bool IsDefault(string petName)
    {
        return false;
    }


    public Collection<PetAttribute> GetAttributes(string petName)
    {
        //todo Multiple Pets:
        // colorable pets
        // welcome window where to set the name
        // prohibit double names
        if (string.IsNullOrWhiteSpace(petName))
            return new Collection<PetAttribute>();

        if (_petCache.TryGetValue(petName, out var pet))
            return new Collection<PetAttribute>(pet.Attributes);

        return new Collection<PetAttribute>();
    }

    public void SetAttribute(string petName, PetAttribute attribute)
    {
        // create pet if not exist
        if (!_petCache.TryGetValue(petName, out var pet))
        {
            pet = new PetData(petName);
            _petCache[petName] = pet;
        }

        var list = pet.Attributes;
        
        // change existing attrributes value && add new ones
        PetAttribute? existing = pet.Attributes.FirstOrDefault(a => a.Name == attribute.Name);


        if (existing != null)
        {
            existing.Value = attribute.Value;
        }
        else
        {
            pet.Attributes.Add(attribute);
        }
        
        SaveToStorage();
    }

    public void RemoveAttribute(string petName, string attributeName)
    {
        if (!_petCache.TryGetValue(petName, out var pet))
            return;

        var list = pet.Attributes;
        int idx = -1;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name == attributeName)
            {
                idx = i;
                break;
            }
        }
        
        if (idx >= 0)
            list.RemoveAt(idx);

        SaveToStorage();
    }

    public void SetDefaultPet(string petName)
    {
        PetData? newDefaultPet = null;
        foreach (var pet in _petCache.Values)
        {
            if (pet.PetName == petName) newDefaultPet = pet;
            pet.IsDefault = false;
        }

        if (newDefaultPet != null)
            newDefaultPet.IsDefault = true;

        SaveToStorage();
    }

    public PetData? GetDefaultPet()
    {
        foreach (var pet in _petCache.Values)
            if (pet.IsDefault)
                return pet;
        return null;
    }

    public PetData? GetPet(string petName)
    {
        foreach (var pet in _petCache.Values)
            if (pet.PetName == petName)
                return pet;
        return null;
    }

    public string GetAttribute(string petName, string attributeName, string defaultValue = "")
    {
        if (!_petCache.TryGetValue(petName, out var pet))
            return defaultValue;

        var list = pet.Attributes;
        foreach (var a in pet.Attributes)
            if (a.Name == attributeName)
                return a.Value;
        
        return defaultValue;
    }

    public void SetAttribute(string petName, string attributeName, string attributeValue)
    {
        SetAttribute(petName, new PetAttribute(attributeName, attributeValue));
    }

    public void SetLastPlayedGame(string petName, string lastPlayedGameName)
    {
        // create pet if not exist
        if (!_petCache.TryGetValue(petName, out var pet))
        {
            pet = new PetData(petName);
            _petCache[petName] = pet;
        }

        var list = pet.LastPlayedGames;

        if (list.Contains(lastPlayedGameName))
            list.Remove(lastPlayedGameName);

        // add front
        list.Insert(0, lastPlayedGameName);

        const int maxEntries = 3;
        if (list.Count > maxEntries)
            list.ToList().RemoveRange(maxEntries, list.Count - maxEntries);

        SaveToStorage();
    }

    public Collection<string> GetLastPlayedGames(string petName)
    {
        if (!_petCache.TryGetValue(petName, out var pet))
            return new Collection<string>();

        return new Collection<string>(pet.LastPlayedGames);
    }
}