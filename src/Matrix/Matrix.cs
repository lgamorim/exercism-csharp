namespace Exercism.Matrix;

public class Matrix
{
    private static readonly char[] RowSeparators = { '\n' };
    private static readonly char[] ValueSeparators = { ' ' };

    private readonly int[][] rows;

    public Matrix(string input)
    {
        rows = input
            .Split(RowSeparators, StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseRow)
            .ToArray();
    }

    public int[] Row(int row)
    {
        return (int[])rows[row - 1].Clone();
    }

    public int[] Column(int column)
    {
        return rows.Select(row => row[column - 1]).ToArray();
    }

    private static int[] ParseRow(string line)
    {
        return line
            .Split(ValueSeparators, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}
