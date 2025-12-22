using System.Windows.Input;

namespace DesktopPet.WPF;

public class PetEventManager
{
    public event KeyEventHandler? KeyDown;
    
    public event MouseButtonEventHandler? MouseDown;
    public event MouseButtonEventHandler? MouseUp;
    
    public void OnKeyDown(object sender, KeyEventArgs e) => KeyDown?.Invoke(sender, e);
    
    public void OnMouseDown(object sender, MouseButtonEventArgs e) => MouseDown?.Invoke(sender, e);
    public void OnMouseUp(object sender, MouseButtonEventArgs e) => MouseUp?.Invoke(sender, e);
    
    public event Action? Pause;
    public event Action? Resume;
    
    public void OnResume() => Resume?.Invoke();
    public void OnPause() => Pause?.Invoke();

    public event Action? CaptureMouse;
    public event Action? ReleaseMouseCapture;
    
    public void OnCaptureMouse() => CaptureMouse?.Invoke();
    public void OnReleaseMouseCapture() => ReleaseMouseCapture?.Invoke();
}