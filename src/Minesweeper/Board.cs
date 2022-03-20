using System.Text;

namespace Exercism.Minesweeper;

public class Board
{
    private const char MineDisplay = '*';
    private const char BlankDisplay = '.';

    private readonly BoardSpace[,] rectangle;

    public Board(int rows, int columns)
    {
        rectangle = new BoardSpace[rows, columns];
    }

    public int Rows => rectangle.GetLength(0);

    public int Columns => rectangle.GetLength(1);

    public void SetMineAt(int row, int column)
    {
        rectangle[row, column] = BoardSpace.Mine;
    }

    public BoardSpace GetSquareAt(int row, int column)
    {
        return rectangle[row, column];
    }

    public int GetTotalAdjacentMines(int row, int column)
    {
        var total = 0;
        for (var i = row - 1; i <= row + 1; i++)
        {
            for (var j = column - 1; j <= column + 1; j++)
            {
                if (i < 0 || i >= Rows || j < 0 || j >= Columns) continue;
                if (HasMineAt(i, j)) total++;
            }
        }

        return total;
    }

    public override string ToString()
    {
        var display = new StringBuilder();
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                var square = DisplaySquare(GetSquareAt(i, j));
                display.Append(square);
            }

            display.Append('\n');
        }

        return display.ToString();
    }

    private static char DisplaySquare(BoardSpace type)
    {
        return type == BoardSpace.Mine ? MineDisplay : BlankDisplay;
    }

    private bool HasMineAt(int row, int column)
    {
        return rectangle[row, column] == BoardSpace.Mine;
    }
}