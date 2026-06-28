using System;
using Xunit;

namespace Exercism.Zipper.UnitTests;

public class ZipperTests
{
    private static BinTree SampleTree()
    {
        return new BinTree(1, new BinTree(2, null, new BinTree(3, null, null)), new BinTree(4, null, null));
    }

    [Fact]
    public void Should_RetainData_When_ConvertingBackToTree()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.ToTree();

        Assert.Equal(SampleTree(), actual);
    }

    [Fact]
    public void Should_ReturnFocusedValue_When_NavigatingLeftThenRight()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Right()?.Value();

        Assert.Equal(3, actual);
    }

    [Fact]
    public void Should_ReturnNull_When_NavigatingPastALeaf()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Left();

        Assert.Null(actual);
    }

    [Fact]
    public void Should_ReturnNull_When_NavigatingRightPastALeaf()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Right()?.Right();

        Assert.Null(actual);
    }

    [Fact]
    public void Should_RebuildWholeTree_When_ConvertingFromADeepFocus()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Right()?.ToTree();

        Assert.Equal(SampleTree(), actual);
    }

    [Fact]
    public void Should_ReturnNull_When_TraversingUpFromTop()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Up();

        Assert.Null(actual);
    }

    [Fact]
    public void Should_ReturnFocusedValue_When_NavigatingLeftRightAndUp()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Up()?.Right()?.Up()?.Left()?.Right()?.Value();

        Assert.Equal(3, actual);
    }

    [Fact]
    public void Should_ReturnRootValue_When_DescendingMultipleLevelsAndReturning()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Right()?.Up()?.Up()?.Value();

        Assert.Equal(1, actual);
    }

    [Fact]
    public void Should_UpdateFocusedValue_When_SettingValue()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.SetValue(5)?.ToTree();

        var expected = new BinTree(1, new BinTree(5, null, new BinTree(3, null, null)), new BinTree(4, null, null));
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_UpdateValue_When_SettingValueAfterTraversingUp()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Right()?.Up()?.SetValue(5)?.ToTree();

        var expected = new BinTree(1, new BinTree(5, null, new BinTree(3, null, null)), new BinTree(4, null, null));
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_AttachSubtree_When_SettingLeftWithLeaf()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.SetLeft(new BinTree(5, null, null))?.ToTree();

        var expected = new BinTree(1, new BinTree(2, new BinTree(5, null, null), new BinTree(3, null, null)), new BinTree(4, null, null));
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_RemoveSubtree_When_SettingRightWithNull()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.SetRight(null)?.ToTree();

        var expected = new BinTree(1, new BinTree(2, null, null), new BinTree(4, null, null));
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_ReplaceSubtree_When_SettingRightWithSubtree()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.SetRight(new BinTree(6, new BinTree(7, null, null), new BinTree(8, null, null)))?.ToTree();

        var expected = new BinTree(1, new BinTree(2, null, new BinTree(3, null, null)), new BinTree(6, new BinTree(7, null, null), new BinTree(8, null, null)));
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_UpdateDeepFocus_When_SettingValueOnDeepFocus()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Right()?.SetValue(5)?.ToTree();

        var expected = new BinTree(1, new BinTree(2, null, new BinTree(5, null, null)), new BinTree(4, null, null));
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_BeEqual_When_ReachedThroughDifferentPaths()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.Up()?.Right();
        var expected = Zipper.FromTree(SampleTree())?.Right();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_ThrowArgumentNullException_When_CreatingFromNullTree()
    {
        Assert.Throws<ArgumentNullException>(() => Zipper.FromTree(null!));
    }

    [Fact]
    public void Should_ConsiderTreesWithEqualStructureEqual_When_BuiltSeparately()
    {
        var first = SampleTree();
        var second = SampleTree();

        Assert.Equal(first, second);
    }

    [Fact]
    public void Should_ConsiderTreesWithDifferentStructureUnequal_When_BuiltSeparately()
    {
        var first = SampleTree();
        var second = new BinTree(1, new BinTree(2, null, new BinTree(99, null, null)), new BinTree(4, null, null));

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Should_KeepRootValueUnchanged_When_SettingValueOnADeepLeafAndReturningToRoot()
    {
        var sut = Zipper.FromTree(SampleTree());

        var rootValue = sut.Left()?.Right()?.SetValue(99)?.Up()?.Up()?.Value();

        Assert.Equal(1, rootValue);
    }

    [Fact]
    public void Should_ReturnNullInEveryDirection_When_TreeHasASingleNode()
    {
        var sut = Zipper.FromTree(new BinTree(1, null, null));

        Assert.Null(sut.Left());
        Assert.Null(sut.Right());
        Assert.Null(sut.Up());
        Assert.Equal(1, sut.Value());
        Assert.Equal(new BinTree(1, null, null), sut.ToTree());
    }

    [Fact]
    public void Should_BeTraversable_When_NavigatingIntoAFreshlySetSubtree()
    {
        var sut = Zipper.FromTree(SampleTree());

        var actual = sut.Left()?.SetLeft(new BinTree(5, null, null))?.Left()?.Value();

        Assert.Equal(5, actual);
    }

    [Fact]
    public void Should_BeUnequal_When_FocusedOnDifferentNodesOfTheSameTree()
    {
        var root = Zipper.FromTree(SampleTree());

        var descendant = root.Left();

        Assert.NotEqual(root, descendant);
    }
}
