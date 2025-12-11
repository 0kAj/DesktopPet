using System.Windows;

namespace DesktopPet.Handlers.LookEvents;

public interface IWindowHelper
{
    public Point GetDpiSaveGlobalMousePos();
    public Vector GetPositionVector();
    public Vector GetCollisionPositionVector();

}