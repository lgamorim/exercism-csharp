namespace Exercism.Forth;

internal sealed class PrimitiveWord(Action<ValueStack> operation) : IWord
{
    public void Execute(ValueStack stack) => operation(stack);
}
