namespace Exercism.Zipper;

internal abstract record Crumb(int Value, BinTree? Sibling);

internal sealed record LeftCrumb(int Value, BinTree? Sibling) : Crumb(Value, Sibling);

internal sealed record RightCrumb(int Value, BinTree? Sibling) : Crumb(Value, Sibling);
