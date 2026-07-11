using System;
using Xunit;

namespace Exercism.Forth.UnitTests;

public class ForthTests
{
    [Fact]
    public void Should_PushNumbersOntoStack_When_ParsingNumbers()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 3 4 5"]);

        Assert.Equal("1 2 3 4 5", actual);
    }

    [Fact]
    public void Should_PushNegativeNumbersOntoStack_When_ParsingNegativeNumbers()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["-1 -2 -3 -4 -5"]);

        Assert.Equal("-1 -2 -3 -4 -5", actual);
    }

    [Fact]
    public void Should_AddTwoNumbers_When_Adding()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 +"]);

        Assert.Equal("3", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_AddingWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["+"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_AddingWithOnlyOneValueOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["1 +"]));
    }

    [Fact]
    public void Should_AddTopTwoValues_When_AddingWithMoreThanTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 3 +"]);

        Assert.Equal("1 5", actual);
    }

    [Fact]
    public void Should_SubtractTwoNumbers_When_Subtracting()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["3 4 -"]);

        Assert.Equal("-1", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_SubtractingWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["-"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_SubtractingWithOnlyOneValueOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["1 -"]));
    }

    [Fact]
    public void Should_SubtractTopTwoValues_When_SubtractingWithMoreThanTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 12 3 -"]);

        Assert.Equal("1 9", actual);
    }

    [Fact]
    public void Should_MultiplyTwoNumbers_When_Multiplying()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["2 4 *"]);

        Assert.Equal("8", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_MultiplyingWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["*"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_MultiplyingWithOnlyOneValueOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["1 *"]));
    }

    [Fact]
    public void Should_MultiplyTopTwoValues_When_MultiplyingWithMoreThanTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 3 *"]);

        Assert.Equal("1 6", actual);
    }

    [Fact]
    public void Should_DivideTwoNumbers_When_Dividing()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["12 3 /"]);

        Assert.Equal("4", actual);
    }

    [Fact]
    public void Should_PerformIntegerDivision_When_Dividing()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["8 3 /"]);

        Assert.Equal("2", actual);
    }

    [Fact]
    public void Should_ThrowDivideByZeroException_When_DividingByZero()
    {
        var sut = new Forth();

        Assert.Throws<DivideByZeroException>(() => sut.Evaluate(["4 0 /"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_DividingWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["/"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_DividingWithOnlyOneValueOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["1 /"]));
    }

    [Fact]
    public void Should_DivideTopTwoValues_When_DividingWithMoreThanTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 12 3 /"]);

        Assert.Equal("1 4", actual);
    }

    [Fact]
    public void Should_ReturnResult_When_CombiningAdditionAndSubtraction()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 + 4 -"]);

        Assert.Equal("-1", actual);
    }

    [Fact]
    public void Should_ReturnResult_When_CombiningMultiplicationAndDivision()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["2 4 * 3 /"]);

        Assert.Equal("2", actual);
    }

    [Fact]
    public void Should_ReturnResult_When_CombiningMultiplicationAndAddition()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 3 4 * +"]);

        Assert.Equal("13", actual);
    }

    [Fact]
    public void Should_ReturnResult_When_CombiningAdditionAndMultiplication()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 3 4 + *"]);

        Assert.Equal("7", actual);
    }

    [Fact]
    public void Should_CopyValue_When_DuplicatingTheOnlyValueOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 dup"]);

        Assert.Equal("1 1", actual);
    }

    [Fact]
    public void Should_CopyTopValue_When_DuplicatingWithMultipleValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 dup"]);

        Assert.Equal("1 2 2", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_DuplicatingWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["dup"]));
    }

    [Fact]
    public void Should_RemoveTopValue_When_DroppingTheOnlyValueOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 drop"]);

        Assert.Equal("", actual);
    }

    [Fact]
    public void Should_RemoveTopValue_When_DroppingWithMultipleValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 drop"]);

        Assert.Equal("1", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_DroppingWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["drop"]));
    }

    [Fact]
    public void Should_SwapTopTwoValues_When_SwappingTheOnlyTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 swap"]);

        Assert.Equal("2 1", actual);
    }

    [Fact]
    public void Should_SwapTopTwoValues_When_SwappingWithMoreThanTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 3 swap"]);

        Assert.Equal("1 3 2", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_SwappingWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["swap"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_SwappingWithOnlyOneValueOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["1 swap"]));
    }

    [Fact]
    public void Should_CopySecondValue_When_ApplyingOverToTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 over"]);

        Assert.Equal("1 2 1", actual);
    }

    [Fact]
    public void Should_CopySecondValue_When_ApplyingOverToMoreThanTwoValuesOnTheStack()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 3 over"]);

        Assert.Equal("1 2 3 2", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_ApplyingOverWithNothingOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["over"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_ApplyingOverWithOnlyOneValueOnTheStack()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["1 over"]));
    }

    [Fact]
    public void Should_ExecuteUserDefinedWord_When_DefinedFromBuiltInWords()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": dup-twice dup dup ;", "1 dup-twice"]);

        Assert.Equal("1 1 1", actual);
    }

    [Fact]
    public void Should_ExecuteInDefinitionOrder_When_ExecutingUserDefinedWord()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": countup 1 2 3 ;", "countup"]);

        Assert.Equal("1 2 3", actual);
    }

    [Fact]
    public void Should_UseLatestDefinition_When_RedefiningUserDefinedWord()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": foo dup ;", ": foo dup dup ;", "1 foo"]);

        Assert.Equal("1 1 1", actual);
    }

    [Fact]
    public void Should_UseUserDefinition_When_OverridingBuiltInWord()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": swap dup ;", "1 swap"]);

        Assert.Equal("1 1", actual);
    }

    [Fact]
    public void Should_UseUserDefinition_When_OverridingBuiltInOperator()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": + * ;", "3 4 +"]);

        Assert.Equal("12", actual);
    }

    [Fact]
    public void Should_KeepOriginalMeaning_When_RedefiningWordUsedByAnotherWord()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": foo 5 ;", ": bar foo ;", ": foo 6 ;", "bar foo"]);

        Assert.Equal("5 6", actual);
    }

    [Fact]
    public void Should_UsePreviousDefinition_When_DefiningWordThatUsesItsOwnName()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": foo 10 ;", ": foo foo 1 + ;", "foo"]);

        Assert.Equal("11", actual);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_RedefiningNonNegativeNumber()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate([": 1 2 ;"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_RedefiningNegativeNumber()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate([": -1 2 ;"]));
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_ExecutingNonExistentWord()
    {
        var sut = new Forth();

        Assert.Throws<InvalidOperationException>(() => sut.Evaluate(["foo"]));
    }

    [Fact]
    public void Should_ScopeDefinitionsToASingleEvaluation_When_OverridingBuiltInOperator()
    {
        var sut = new Forth();

        var withOverride = sut.Evaluate([": + - ;", "1 1 +"]);
        var withoutOverride = sut.Evaluate(["1 1 +"]);

        Assert.Equal("0", withOverride);
        Assert.Equal("2", withoutOverride);
    }

    [Fact]
    public void Should_IgnoreCase_When_ExecutingDup()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 DUP Dup dup"]);

        Assert.Equal("1 1 1 1", actual);
    }

    [Fact]
    public void Should_IgnoreCase_When_ExecutingDrop()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 3 4 DROP Drop drop"]);

        Assert.Equal("1", actual);
    }

    [Fact]
    public void Should_IgnoreCase_When_ExecutingSwap()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 SWAP 3 Swap 4 swap"]);

        Assert.Equal("2 3 4 1", actual);
    }

    [Fact]
    public void Should_IgnoreCase_When_ExecutingOver()
    {
        var sut = new Forth();

        var actual = sut.Evaluate(["1 2 OVER Over over"]);

        Assert.Equal("1 2 1 2 1", actual);
    }

    [Fact]
    public void Should_IgnoreCase_When_ExecutingUserDefinedWord()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": foo dup ;", "1 FOO Foo foo"]);

        Assert.Equal("1 1 1 1", actual);
    }

    [Fact]
    public void Should_IgnoreCase_When_DefiningWords()
    {
        var sut = new Forth();

        var actual = sut.Evaluate([": SWAP DUP Dup dup ;", "1 swap"]);

        Assert.Equal("1 1 1 1", actual);
    }

    [Fact]
    public void Should_ThrowArgumentNullException_When_EvaluatingNullInstructions()
    {
        var sut = new Forth();

        Assert.Throws<ArgumentNullException>(() => sut.Evaluate(null!));
    }
}
