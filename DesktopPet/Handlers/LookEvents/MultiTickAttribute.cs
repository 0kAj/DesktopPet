namespace DesktopPet.Handlers.LookEvents;

public class MultiTickAttribute
{
    private readonly bool _pingPong;
    private readonly double _speed;
    private double _direction;

    private double _progress;

    public MultiTickAttribute(double speed = 0.15, bool pingPong = false, double direction = 1)
    {
        _speed = speed;
        _pingPong = pingPong;
        _direction = direction;
    }

    public bool IsChanging { get; private set; }
    public double StartValue { get; private set; }
    public double EndValue { get; private set; }

    public double Tick(double currentValue, double target)
    {
        // restart if target Changed
        if (!IsChanging)
        {
            StartValue = currentValue;
            EndValue = target;

            _progress = 0.0;
            _direction = 1.0;
            IsChanging = true;
        }

        _progress += _speed * _direction;

        // Ping-Pong
        if (_pingPong)
        {
            if (_progress >= 1.0) _direction = -1.0;

            if (_progress <= 0.0)
            {
                IsChanging = false;
                _progress = 0.0;
            }
        }
        else
        {
            if (_progress >= 1.0)
            {
                _progress = 1.0;
                IsChanging = false;
            }
        }

        return Lerp(StartValue, EndValue, _progress);
    }

    private double Lerp(double a, double b, double t)
    {
        return a + (b - a) * t;
    }
}