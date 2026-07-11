namespace Exercism.Forth;

internal sealed class Interpreter
{
    private readonly ValueStack stack = new();
    private readonly Dictionary<string, IWord> builtIns;
    private readonly Dictionary<string, IWord> definitions = new();

    public Interpreter()
    {
        builtIns = new Dictionary<string, IWord>
        {
            ["+"] = BinaryOperator((left, right) => left + right),
            ["-"] = BinaryOperator((left, right) => left - right),
            ["*"] = BinaryOperator((left, right) => left * right),
            ["/"] = BinaryOperator((left, right) => left / right),
            ["dup"] = new PrimitiveWord(s => s.Push(s.Peek())),
            ["drop"] = new PrimitiveWord(s => s.Pop()),
            ["swap"] = new PrimitiveWord(Swap),
            ["over"] = new PrimitiveWord(Over)
        };
    }

    public string StackContents => stack.Contents;

    public void Interpret(string instruction)
    {
        var tokens = instruction.ToLowerInvariant()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (var i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == ":")
                i = Define(tokens, i);
            else
                Resolve(tokens[i]).Execute(stack);
        }
    }

    private int Define(string[] tokens, int start)
    {
        var name = tokens[start + 1];
        if (int.TryParse(name, out _))
            throw new InvalidOperationException($"Cannot redefine number: {name}");

        // Resolving at definition time means later redefinitions of the words
        // used in the body do not change this word's behaviour.
        var body = new List<IWord>();
        var i = start + 2;
        for (; tokens[i] != ";"; i++)
            body.Add(Resolve(tokens[i]));

        definitions[name] = new CompositeWord(body);
        return i;
    }

    private IWord Resolve(string token)
    {
        if (definitions.TryGetValue(token, out var defined))
            return defined;

        if (builtIns.TryGetValue(token, out var builtIn))
            return builtIn;

        if (int.TryParse(token, out var number))
            return new NumberWord(number);

        throw new InvalidOperationException($"Undefined word: {token}");
    }

    private static PrimitiveWord BinaryOperator(Func<int, int, int> operation) =>
        new(stack =>
        {
            var right = stack.Pop();
            var left = stack.Pop();
            stack.Push(operation(left, right));
        });

    private static void Swap(ValueStack stack)
    {
        var top = stack.Pop();
        var second = stack.Pop();
        stack.Push(top);
        stack.Push(second);
    }

    private static void Over(ValueStack stack)
    {
        var top = stack.Pop();
        var second = stack.Peek();
        stack.Push(top);
        stack.Push(second);
    }
}
