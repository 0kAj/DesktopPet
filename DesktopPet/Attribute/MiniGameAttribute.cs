namespace DesktopPet.Attribute;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MiniGameAttribute : System.Attribute
{
    public MiniGameAttribute(string gameName)
    {
        GameName = gameName;
    }

    public string GameName { get; }
}