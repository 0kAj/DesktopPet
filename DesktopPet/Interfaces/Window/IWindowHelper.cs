using System.Windows;

namespace DesktopPet.Interfaces.Window;

public interface IWindowHelper
{
    public double Width { get; }
    public double Height { get; }
    public double Left { get; }
    public double Top { get; }
    public Point GetDpiSaveGlobalMousePos();
    public Vector GetPositionVector();
    public Vector GetCollisionPositionVector();
}