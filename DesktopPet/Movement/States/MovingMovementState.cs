using System;
using System.Windows;
using DesktopPet.Interfaces;
using DesktopPet.UI;

namespace DesktopPet.Movement.States
{
    public class MovingMovementState : IBehaviourState
    {
        private readonly PetWindow _petWindow;
        private double _targetX;
        private bool _isDone;

        public bool IsDone => _isDone;

        public MovingMovementState(PetWindow petWindow)
        {
            _petWindow = petWindow;

            GenerateRadomTargetX();
        }

        private void GenerateRadomTargetX()
        {
            var screen = SystemParameters.WorkArea;

            // Zufällige Position auf der Taskleiste
            var random = new Random(DateTime.Now.Millisecond);
            _targetX = random.Next((int)screen.Left, (int)(screen.Right - _petWindow.Width));
            
            _petWindow.debugLabel.Content = _targetX.ToString();
            _isDone = false;
        }

        public bool CanTick() => !_isDone && _petWindow.IsOnGround;

        public void Tick()
        {
            if (_isDone)
                return;

            var distance = _targetX - _petWindow.Left;
            double direction = Math.Sign(distance);

            // _petWindow.Left += direction * Speed;
            _petWindow.VelocityX = direction * _petWindow.Speed;

            if (Math.Abs(distance) < 5)
            {
                // _petWindow.Left = _targetX;
                // _petWindow.VelocityX = 0;
                _isDone = true;
                GenerateRadomTargetX();
            }
        }

        public void OnEnd()
        {
            
        }
    }
}