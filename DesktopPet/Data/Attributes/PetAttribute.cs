using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesktopPet.Data.Attributes;

public class PetAttribute : INotifyPropertyChanged
{
    private string _name;

    private string _value;

    public PetAttribute(string name, string value)
    {
        _name = name;
        _value = value;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}