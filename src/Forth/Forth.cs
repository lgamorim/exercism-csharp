namespace Exercism.Forth;

public sealed class Forth
{
    public string Evaluate(IEnumerable<string> instructions)
    {
        ArgumentNullException.ThrowIfNull(instructions);

        var interpreter = new Interpreter();
        foreach (var instruction in instructions)
            interpreter.Interpret(instruction);

        return interpreter.StackContents;
    }
}
