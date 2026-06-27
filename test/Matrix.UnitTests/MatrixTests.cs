using Xunit;

namespace Exercism.Matrix.UnitTests;

public class MatrixTests
{
    [Fact]
    public void Should_ReturnSingleValue_When_ExtractingRowFromOneNumberMatrix()
    {
        var matrix = new Matrix("1");

        Assert.Equal(new[] { 1 }, matrix.Row(1));
    }

    [Fact]
    public void Should_ReturnSecondRow_When_ExtractingRow()
    {
        var matrix = new Matrix("1 2\n3 4");

        Assert.Equal(new[] { 3, 4 }, matrix.Row(2));
    }

    [Fact]
    public void Should_ReturnRow_When_NumbersHaveDifferentWidths()
    {
        var matrix = new Matrix("1 2\n10 20");

        Assert.Equal(new[] { 10, 20 }, matrix.Row(2));
    }

    [Fact]
    public void Should_ReturnRow_When_MatrixIsNonSquareWithNoCorrespondingColumn()
    {
        var matrix = new Matrix("1 2 3\n4 5 6\n7 8 9\n8 7 6");

        Assert.Equal(new[] { 8, 7, 6 }, matrix.Row(4));
    }

    [Fact]
    public void Should_ReturnSingleValue_When_ExtractingColumnFromOneNumberMatrix()
    {
        var matrix = new Matrix("1");

        Assert.Equal(new[] { 1 }, matrix.Column(1));
    }

    [Fact]
    public void Should_ReturnThirdColumn_When_ExtractingColumn()
    {
        var matrix = new Matrix("1 2 3\n4 5 6\n7 8 9");

        Assert.Equal(new[] { 3, 6, 9 }, matrix.Column(3));
    }

    [Fact]
    public void Should_ReturnColumn_When_MatrixIsNonSquareWithNoCorrespondingRow()
    {
        var matrix = new Matrix("1 2 3 4\n5 6 7 8\n9 8 7 6");

        Assert.Equal(new[] { 4, 8, 6 }, matrix.Column(4));
    }

    [Fact]
    public void Should_ReturnColumn_When_NumbersHaveDifferentWidths()
    {
        var matrix = new Matrix("89 1903 3\n18 3 1\n9 4 800");

        Assert.Equal(new[] { 1903, 3, 4 }, matrix.Column(2));
    }
}
