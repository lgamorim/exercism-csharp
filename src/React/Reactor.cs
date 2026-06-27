namespace Exercism.React;

public class Reactor
{
    private readonly List<ComputeCell> computeCells = new();

    public InputCell CreateInputCell(int value)
    {
        return new InputCell(this, value);
    }

    public ComputeCell CreateComputeCell(IEnumerable<Cell> inputs, Func<int[], int> compute)
    {
        ArgumentNullException.ThrowIfNull(inputs);
        ArgumentNullException.ThrowIfNull(compute);

        var computeCell = new ComputeCell(inputs, compute);
        computeCells.Add(computeCell);
        return computeCell;
    }

    // Assumes callbacks do not write back into the reactor (e.g. set an
    // InputCell.Value) during propagation; doing so would trigger a nested
    // Propagate() while this loop is still running.
    internal void Propagate()
    {
        foreach (var computeCell in computeCells)
            computeCell.Recompute();
    }
}
