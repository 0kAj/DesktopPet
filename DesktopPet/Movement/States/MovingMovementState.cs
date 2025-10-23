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
        private const double Speed = 100;
        private bool _isDone;

        public bool IsDone => _isDone;

        public MovingMovementState(PetWindow petWindow)
        {
            this._petWindow = petWindow;
        }

        public void OnStart()
        {
            var screen = SystemParameters.WorkArea;

            // Zufällige Position auf der Taskleiste
            var random = new Random(DateTime.Now.Millisecond);
            _targetX = random.Next((int)screen.Left, (int)(screen.Right - _petWindow.Width));
            
            _petWindow.debugLabel.Content = _targetX.ToString();
            
            _isDone = false;
        }

        public bool CanTick() => !_isDone && _petWindow.IsOnGround;

        public void Tick(double deltaTime)
        {
            if (_isDone)
                return;

            double direction = Math.Sign(_targetX - _petWindow.Left);

            _petWindow.Left += direction * Speed * deltaTime;

            if ((direction > 0 && _petWindow.Left >= _targetX) ||
                (direction < 0 && _petWindow.Left <= _targetX))
            {
                _petWindow.Left = _targetX;
                _isDone = true;
                OnEnd();
            }
        }

        public void OnEnd()
        {
            OnStart(); // looping
        }
    }
}