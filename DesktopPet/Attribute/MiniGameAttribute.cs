namespace DesktopPet.Attribute;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MiniGameAttribute : System.Attribute
{
    public string GameName { get; }
    public MiniGameAttribute(string gameName) => GameName = gameName;
}
