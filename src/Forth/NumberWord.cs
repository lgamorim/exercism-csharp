namespace Exercism.Forth;

internal sealed class NumberWord(int value) : IWord
{
    public void Execute(ValueStack stack) => stack.Push(value);
}
