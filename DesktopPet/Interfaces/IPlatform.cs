using System.Windows;

namespace DesktopPet.Interfaces;

public interface IPlatform
{
    Rect GetCollisionRect();
    void Update();
}
