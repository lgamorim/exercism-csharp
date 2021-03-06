using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Exercism.Minesweeper.UnitTests;

public class BoardTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public BoardTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Should_SetRectangleWithRowsAndColumns_When_NewBoardIsCreated()
    {
        const int rows = 4;
        const int columns = 5;

        var board = new Board(rows, columns);

        board.TotalRows.Should().Be(rows);
        board.TotalColumns.Should().Be(columns);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 2)]
    public void Should_GetSquareAsMine_When_SetMineAtSquare(int row, int column)
    {
        var board = new Board(4, 5);

        board.SetMineAt(row, column);

        board.GetSquareAt(row, column).Should().Be(BoardSpace.Mine);
    }

    [Fact]
    public void Should_GetAnyOtherSquareAsBlank_After_SetBoardMines()
    {
        var board = new Board(4, 5);

        board.SetMineAt(0, 1);
        board.SetMineAt(0, 3);
        board.SetMineAt(1, 2);
        board.SetMineAt(2, 2);

        board.GetSquareAt(0, 0).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(0, 2).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(0, 4).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(1, 0).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(1, 1).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(1, 3).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(1, 4).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(2, 0).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(2, 1).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(2, 3).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(2, 4).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(3, 0).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(3, 1).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(3, 2).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(3, 3).Should().Be(BoardSpace.Blank);
        board.GetSquareAt(3, 4).Should().Be(BoardSpace.Blank);
    }

    [Fact]
    public void Should_GetCorrectCountOfAdjacentMines_When_GetTotalAdjacentMines()
    {
        var board = new Board(4, 5);

        board.SetMineAt(0, 1);
        board.SetMineAt(0, 3);
        board.SetMineAt(1, 2);
        board.SetMineAt(2, 2);
        
        board.GetTotalAdjacentMines(0, 0).Should().Be(1);
        board.GetTotalAdjacentMines(0, 2).Should().Be(3);
        board.GetTotalAdjacentMines(0, 4).Should().Be(1);
        board.GetTotalAdjacentMines(1, 0).Should().Be(1);
        board.GetTotalAdjacentMines(1, 1).Should().Be(3);
        board.GetTotalAdjacentMines(1, 3).Should().Be(3);
        board.GetTotalAdjacentMines(1, 4).Should().Be(1);
        board.GetTotalAdjacentMines(2, 0).Should().Be(0);
        board.GetTotalAdjacentMines(2, 1).Should().Be(2);
        board.GetTotalAdjacentMines(2, 3).Should().Be(2);
        board.GetTotalAdjacentMines(2, 4).Should().Be(0);
        board.GetTotalAdjacentMines(3, 0).Should().Be(0);
        board.GetTotalAdjacentMines(3, 1).Should().Be(1);
        board.GetTotalAdjacentMines(3, 2).Should().Be(1);
        board.GetTotalAdjacentMines(3, 3).Should().Be(1);
        board.GetTotalAdjacentMines(3, 4).Should().Be(0);
    }

    [Fact]
    public void Should_DisplayBlankPerDotAndAsteriskPerMine_When_ToString()
    {
        const string expected = ".*.*.\n..*..\n..*..\n.....\n";
        var board = new Board(4, 5);

        board.SetMineAt(0, 1);
        board.SetMineAt(0, 3);
        board.SetMineAt(1, 2);
        board.SetMineAt(2, 2);

        var display = board.ToString();
        
        testOutputHelper.WriteLine(display);
        display.Should().Be(expected);
    }

    [Fact]
    public void Should_ReplaceMinesAdjacentWithCount_When_ApplyingTransform()
    {
        const string expected = "1*3*1\n13*31\n.2*2.\n.111.\n";
        var board = new Board(4, 5);

        board.SetMineAt(0, 1);
        board.SetMineAt(0, 3);
        board.SetMineAt(1, 2);
        board.SetMineAt(2, 2);

        var display = board.Transform();
        
        testOutputHelper.WriteLine(display);
        display.Should().Be(expected);
    }
}