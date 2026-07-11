namespace Exercism.Forth;

internal interface IWord
{
    void Execute(ValueStack stack);
}
