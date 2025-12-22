using System.Windows;

namespace DesktopPet.Interfaces.Window;

public interface IWindowHelper
{
    public Point GetDpiSaveGlobalMousePos();
    public Vector GetPositionVector();
    public Vector GetCollisionPositionVector();
}