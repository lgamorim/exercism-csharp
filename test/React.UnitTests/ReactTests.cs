using System;
using System.Collections.Generic;
using Xunit;

namespace Exercism.React.UnitTests;

public class ReactTests
{
    [Fact]
    public void Should_ReturnInitialValue_When_CreatingInputCell()
    {
        var reactor = new Reactor();

        var input = reactor.CreateInputCell(10);

        Assert.Equal(10, input.Value);
    }

    [Fact]
    public void Should_UpdateValue_When_SettingInputCell()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(4);

        input.Value = 20;

        Assert.Equal(20, input.Value);
    }

    [Fact]
    public void Should_CalculateInitialValue_When_CreatingComputeCell()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);

        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);

        Assert.Equal(2, output.Value);
    }

    [Fact]
    public void Should_PassInputsInOrder_When_ComputingWithMultipleInputs()
    {
        var reactor = new Reactor();
        var one = reactor.CreateInputCell(1);
        var two = reactor.CreateInputCell(2);

        var output = reactor.CreateComputeCell(new[] { one, two }, inputs => inputs[0] + inputs[1] * 10);

        Assert.Equal(21, output.Value);
    }

    [Fact]
    public void Should_UpdateComputeCellValue_When_DependencyChanges()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);

        input.Value = 3;

        Assert.Equal(4, output.Value);
    }

    [Fact]
    public void Should_UpdateValue_When_ComputeCellDependsOnOtherComputeCells()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var timesTwo = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] * 2);
        var timesThirty = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] * 30);
        var output = reactor.CreateComputeCell(new[] { timesTwo, timesThirty }, inputs => inputs[0] + inputs[1]);

        Assert.Equal(32, output.Value);

        input.Value = 3;

        Assert.Equal(96, output.Value);
    }

    [Fact]
    public void Should_NotPropagate_When_SettingInputCellToItsCurrentValue()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(5);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var notifications = 0;
        output.Changed += (_, _) => notifications++;

        input.Value = 5;

        Assert.Equal(0, notifications);
    }

    [Fact]
    public void Should_InvokeCallback_When_ComputeCellValueChanges()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var receivedValues = new List<int>();
        output.Changed += (_, value) => receivedValues.Add(value);

        input.Value = 3;

        Assert.Equal(new[] { 4 }, receivedValues);
    }

    [Fact]
    public void Should_PassComputeCellAsSender_When_InvokingCallback()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        object? sender = null;
        output.Changed += (s, _) => sender = s;

        input.Value = 3;

        Assert.Same(output, sender);
    }

    [Fact]
    public void Should_NotInvokeCallback_When_ComputeCellValueStaysTheSame()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] < 3 ? 111 : 222);
        var receivedValues = new List<int>();
        output.Changed += (_, value) => receivedValues.Add(value);

        input.Value = 2;

        Assert.Empty(receivedValues);

        input.Value = 4;

        Assert.Equal(new[] { 222 }, receivedValues);
    }

    [Fact]
    public void Should_FireOnceForEachChange_When_ValueChangesRepeatedly()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var receivedValues = new List<int>();
        output.Changed += (_, value) => receivedValues.Add(value);

        input.Value = 2;
        input.Value = 3;

        Assert.Equal(new[] { 3, 4 }, receivedValues);
    }

    [Fact]
    public void Should_InvokeEachCallback_When_MultipleComputeCellsDependOnSameInput()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var plusOne = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var minusOne = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] - 1);
        var plusOneValues = new List<int>();
        plusOne.Changed += (_, value) => plusOneValues.Add(value);
        var minusOneValues = new List<int>();
        minusOne.Changed += (_, value) => minusOneValues.Add(value);

        input.Value = 10;

        Assert.Equal(new[] { 11 }, plusOneValues);
        Assert.Equal(new[] { 9 }, minusOneValues);
    }

    [Fact]
    public void Should_StopInvokingCallback_When_CallbackIsRemoved()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(11);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var callback1Values = new List<int>();
        EventHandler<int> callback1 = (_, value) => callback1Values.Add(value);
        var callback2Values = new List<int>();
        EventHandler<int> callback2 = (_, value) => callback2Values.Add(value);
        output.Changed += callback1;
        output.Changed += callback2;

        input.Value = 31;

        Assert.Equal(new[] { 32 }, callback1Values);
        Assert.Equal(new[] { 32 }, callback2Values);

        output.Changed -= callback1;
        input.Value = 41;

        Assert.Equal(new[] { 32 }, callback1Values);
        Assert.Equal(new[] { 32, 42 }, callback2Values);
    }

    [Fact]
    public void Should_NotInterfereWithOtherCallbacks_When_RemovingCallbackMultipleTimes()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var output = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var callback1Values = new List<int>();
        EventHandler<int> callback1 = (_, value) => callback1Values.Add(value);
        var callback2Values = new List<int>();
        EventHandler<int> callback2 = (_, value) => callback2Values.Add(value);
        output.Changed += callback1;
        output.Changed += callback2;

        output.Changed -= callback1;
        output.Changed -= callback1;
        output.Changed -= callback1;
        input.Value = 2;

        Assert.Empty(callback1Values);
        Assert.Equal(new[] { 3 }, callback2Values);
    }

    [Fact]
    public void Should_InvokeCallbackOnlyOnce_When_MultipleDependenciesChangeForSameComputeCell()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var plusOne = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var minusOne1 = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] - 1);
        var minusOne2 = reactor.CreateComputeCell(new[] { minusOne1 }, inputs => inputs[0] - 1);
        var output = reactor.CreateComputeCell(new[] { plusOne, minusOne2 }, inputs => inputs[0] * inputs[1]);
        var receivedValues = new List<int>();
        output.Changed += (_, value) => receivedValues.Add(value);

        input.Value = 4;

        Assert.Equal(new[] { 10 }, receivedValues);
    }

    [Fact]
    public void Should_NotInvokeCallback_When_DependenciesChangeButOutputValueDoesNotChange()
    {
        var reactor = new Reactor();
        var input = reactor.CreateInputCell(1);
        var plusOne = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] + 1);
        var minusOne = reactor.CreateComputeCell(new[] { input }, inputs => inputs[0] - 1);
        var alwaysTwo = reactor.CreateComputeCell(new[] { plusOne, minusOne }, inputs => inputs[0] - inputs[1]);
        var receivedValues = new List<int>();
        alwaysTwo.Changed += (_, value) => receivedValues.Add(value);

        input.Value = 2;
        input.Value = 3;
        input.Value = 4;
        input.Value = 5;

        Assert.Empty(receivedValues);
    }

    [Fact]
    public void Should_KeepInputCellsIndependent_When_MultipleInputCellsAreCreated()
    {
        var reactor = new Reactor();
        var first = reactor.CreateInputCell(1);
        var second = reactor.CreateInputCell(2);

        first.Value = 100;

        Assert.Equal(100, first.Value);
        Assert.Equal(2, second.Value);
    }
}
