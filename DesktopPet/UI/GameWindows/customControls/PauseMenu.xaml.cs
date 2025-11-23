using System.Windows.Controls;

namespace DesktopPet.UI.GameWindows.customControls;

public partial class PauseMenu : UserControl
{
    public event Action ResumeClicked;
    // public event Action RestartClicked;
    public event Action LeaveClicked;

    public PauseMenu()
    {
        InitializeComponent();
        ResumeButton.Click += (_,_) => ResumeClicked!.Invoke();
        // RestartButton.Click += (_,_) => RestartClicked!.Invoke();
        LeaveButton.Click += (_,_) => LeaveClicked!.Invoke();
    }
}
