using System.Windows;
using DesktopPet.Handlers.LookEvents;
using DesktopPet.Interfaces;

namespace DesktopPet.Handlers.MovementStates;

public class MovingMovementState : IBehaviourState
{
    private readonly PetBrain _brain;
    private readonly PetViewModel _petViewModel;
    private double _targetX;

    public MovingMovementState(PetBrain petBrain)
    {
        _brain = petBrain;
        _petViewModel = petBrain.PetViewModel;

        GenerateRadomTargetX();
    }

    public bool IsDone { get; private set; }

    public bool CanTick()
    {
        return !IsDone && _brain.IsOnGround;
    }

    public void Tick()
    {
        if (IsDone)
            return;

        var distance = _targetX - _petViewModel.Left;
        double direction = Math.Sign(distance);

        // _petWindow.Left += direction * Speed;
        _petViewModel.VelocityX = direction * _brain.Speed;

        if (Math.Abs(distance) < 5)
        {
            // _petWindow.Left = _targetX;
            // _petWindow.VelocityX = 0;
            IsDone = true;
            GenerateRadomTargetX();
        }
    }

    public void OnEnd()
    {
    }

    public void UnRegister()
    {
    }

    private void GenerateRadomTargetX()
    {
        var screen = SystemParameters.WorkArea;

        // Zufällige Position auf der Taskleiste
        var random = new Random(DateTime.Now.Millisecond);
        _targetX = random.Next((int)screen.Left, (int)(screen.Right - _petViewModel.WindowWidth));

        // _petWindow.debugLabel.Content = _targetX.ToString();
        IsDone = false;
    }
}