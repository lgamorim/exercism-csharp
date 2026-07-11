namespace Exercism.Forth;

internal sealed class ValueStack
{
    private readonly Stack<int> values = new();

    public string Contents => string.Join(" ", values.Reverse());

    public void Push(int value) => values.Push(value);

    public int Pop() =>
        values.Count > 0 ? values.Pop() : throw new InvalidOperationException("Stack is empty");

    public int Peek() =>
        values.Count > 0 ? values.Peek() : throw new InvalidOperationException("Stack is empty");
}
