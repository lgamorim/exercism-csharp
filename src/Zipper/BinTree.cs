namespace Exercism.Zipper;

public sealed record BinTree(int Value, BinTree? Left, BinTree? Right);
