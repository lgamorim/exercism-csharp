namespace Exercism.React;

public class ComputeCell : Cell
{
    private readonly Cell[] inputs;
    private readonly Func<int[], int> compute;
    private int currentValue;

    internal ComputeCell(IEnumerable<Cell> inputs, Func<int[], int> compute)
    {
        ArgumentNullException.ThrowIfNull(inputs);
        ArgumentNullException.ThrowIfNull(compute);

        this.inputs = inputs.ToArray();
        this.compute = compute;
        currentValue = Calculate();
    }

    public int Value => currentValue;

    internal override int CurrentValue => currentValue;

    public event EventHandler<int>? Changed;

    internal void Recompute()
    {
        var updatedValue = Calculate();
        if (updatedValue == currentValue)
            return;

        currentValue = updatedValue;
        Changed?.Invoke(this, updatedValue);
    }

    private int Calculate()
    {
        return compute(inputs.Select(input => input.CurrentValue).ToArray());
    }
}
