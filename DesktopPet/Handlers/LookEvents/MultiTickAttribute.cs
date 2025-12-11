namespace DesktopPet.Handlers.LookEvents;

public class MultiTickAttribute<T>
{
    public bool IsChanging = false;
    public T StartValue { get; private set; }
    public T EndValue { get; private set; }

    public MultiTickAttribute(T startValue, T endValue)
    {
        StartValue = startValue;
        EndValue = endValue;
    }
}