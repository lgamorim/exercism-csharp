using System.Collections.Immutable;

namespace Exercism.Zipper;

public sealed class Zipper : IEquatable<Zipper>
{
    private readonly int value;
    private readonly BinTree? left;
    private readonly BinTree? right;
    private readonly ImmutableStack<Crumb> crumbs;

    private Zipper(int value, BinTree? left, BinTree? right, ImmutableStack<Crumb> crumbs)
    {
        this.value = value;
        this.left = left;
        this.right = right;
        this.crumbs = crumbs;
    }

    public static Zipper FromTree(BinTree tree)
    {
        ArgumentNullException.ThrowIfNull(tree);

        return new Zipper(tree.Value, tree.Left, tree.Right, ImmutableStack<Crumb>.Empty);
    }

    public int Value() => value;

    public Zipper SetValue(int newValue) => new(newValue, left, right, crumbs);

    public Zipper SetLeft(BinTree? subtree) => new(value, subtree, right, crumbs);

    public Zipper SetRight(BinTree? subtree) => new(value, left, subtree, crumbs);

    public Zipper? Left()
    {
        if (left is null)
            return null;

        var newCrumbs = crumbs.Push(new LeftCrumb(value, right));
        return new Zipper(left.Value, left.Left, left.Right, newCrumbs);
    }

    public Zipper? Right()
    {
        if (right is null)
            return null;

        var newCrumbs = crumbs.Push(new RightCrumb(value, left));
        return new Zipper(right.Value, right.Left, right.Right, newCrumbs);
    }

    public Zipper? Up()
    {
        if (crumbs.IsEmpty)
            return null;

        var remainingCrumbs = crumbs.Pop(out var crumb);
        var tree = new BinTree(value, left, right);

        return crumb switch
        {
            LeftCrumb => new Zipper(crumb.Value, tree, crumb.Sibling, remainingCrumbs),
            RightCrumb => new Zipper(crumb.Value, crumb.Sibling, tree, remainingCrumbs),
            _ => throw new InvalidOperationException($"Unknown crumb type: {crumb.GetType()}")
        };
    }

    public BinTree ToTree()
    {
        var tree = new BinTree(value, left, right);

        foreach (var crumb in crumbs)
        {
            tree = crumb switch
            {
                LeftCrumb => new BinTree(crumb.Value, tree, crumb.Sibling),
                RightCrumb => new BinTree(crumb.Value, crumb.Sibling, tree),
                _ => throw new InvalidOperationException($"Unknown crumb type: {crumb.GetType()}")
            };
        }

        return tree;
    }

    public bool Equals(Zipper? other) =>
        other is not null &&
        value == other.value &&
        Equals(left, other.left) &&
        Equals(right, other.right) &&
        crumbs.SequenceEqual(other.crumbs);

    public override bool Equals(object? obj) => Equals(obj as Zipper);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(value);
        hash.Add(left);
        hash.Add(right);
        foreach (var crumb in crumbs)
            hash.Add(crumb);

        return hash.ToHashCode();
    }
}
