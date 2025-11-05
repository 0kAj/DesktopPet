using System.IO;
using System.Text.Json;
using System.Windows;

namespace DesktopPet.Data;

//help: https://learn.microsoft.com/de-de/dotnet/standard/serialization/system-text-json/how-to
public class DataManager
{
    public static readonly DataManager Instance = new DataManager();
    
    // folder in %appdata%
    private static readonly string DataDirectoryPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "DesktopPet", "data"
    );

    private static string GetDataPath<T>()
    {
        return Path.Combine(DataDirectoryPath, typeof(T).Name + ".json");
    }

    // save T , jsonDataString
    private Dictionary<Type, string> _jsonDataStrings = new();
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true};
    
    
    public T GetData<T>() where T : new()
    {
        if (_jsonDataStrings.ContainsKey(typeof(T)))
        {
            var d = JsonSerializer.Deserialize<T>(_jsonDataStrings[typeof(T)], _options);
            if (d != null)
                return d;
            
            MessageBox.Show("Json Error: Could not get loaded data of Type: " + typeof(T).Name);
        }
        
        // Load Data
        if (!Directory.Exists(DataDirectoryPath))
            Directory.CreateDirectory(DataDirectoryPath);
        
        if (!File.Exists(GetDataPath<T>()))
        {
            var defaultData = new T();
            SaveData(defaultData); // create Dummy file
            return defaultData;
        }

        var jsonDataString = File.ReadAllText(GetDataPath<T>());
        
        _jsonDataStrings[typeof(T)] = jsonDataString; // caching of data
        
        var data = JsonSerializer.Deserialize<T>(jsonDataString, _options);
        if (data == null)
            return new T();
        return data; // if !File.Exists(GetDataPath<T>())
    }

    public void SaveData<T>(T data)
    {
        _jsonDataStrings[typeof(T)] = JsonSerializer.Serialize<T>(data, _options);

        if (!Directory.Exists(DataDirectoryPath))
            Directory.CreateDirectory(DataDirectoryPath);
        File.WriteAllText(GetDataPath<T>(), _jsonDataStrings[typeof(T)]);
    }
}