using FluentAssertions;
using Xunit;

namespace Exercism.Minesweeper.UnitTests;

public class BoardTests
{
    [Fact]
    public void Should_SetRectangleWithRowsAndColumns_When_NewBoardIsCreated()
    {
        const int rows = 4;
        const int columns = 5;

        var board = new Board(rows, columns);

        board.Rows.Should().Be(rows);
        board.Columns.Should().Be(columns);
    }
}