namespace Exercism.Forth;

internal sealed class CompositeWord(IReadOnlyList<IWord> words) : IWord
{
    public void Execute(ValueStack stack)
    {
        foreach (var word in words)
            word.Execute(stack);
    }
}
