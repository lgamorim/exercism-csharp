namespace Exercism.React;

public class InputCell : Cell
{
    private readonly Reactor reactor;
    private int currentValue;

    internal InputCell(Reactor reactor, int value)
    {
        this.reactor = reactor;
        currentValue = value;
    }

    public int Value
    {
        get => currentValue;
        set
        {
            if (value == currentValue)
                return;

            currentValue = value;
            reactor.Propagate();
        }
    }

    internal override int CurrentValue => currentValue;
}
