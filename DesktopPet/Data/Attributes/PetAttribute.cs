namespace DesktopPet.Data.Attributes;

public class PetAttribute
{
    public string Name { get; set; } = "";
    public string Value { get; set; } = "";

    public PetAttribute(string name, string value)
    {
        Name = name;
        Value = value;
    }
}