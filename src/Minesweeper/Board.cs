namespace Exercism.Minesweeper;

public class Board
{
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
}