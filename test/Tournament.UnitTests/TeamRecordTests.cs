using FluentAssertions;
using Xunit;

namespace Exercism.Tournament.UnitTests;

public class TeamRecordTests
{
    [Fact]
    public void Should_SetNameWithNoMatchesPlayed_When_NewTeamRecordIsCreated()
    {
        var record = new TeamRecord("Allegoric Alaskans");

        record.Name.Should().Be("Allegoric Alaskans");
        record.Wins.Should().Be(0);
        record.Draws.Should().Be(0);
        record.Losses.Should().Be(0);
        record.MatchesPlayed.Should().Be(0);
        record.Points.Should().Be(0);
    }

    [Fact]
    public void Should_IncrementWinsAndMatchesPlayed_When_AddWin()
    {
        var record = new TeamRecord("Allegoric Alaskans");

        record.AddWin();

        record.Wins.Should().Be(1);
        record.MatchesPlayed.Should().Be(1);
        record.Points.Should().Be(3);
    }

    [Fact]
    public void Should_IncrementDrawsAndMatchesPlayed_When_AddDraw()
    {
        var record = new TeamRecord("Allegoric Alaskans");

        record.AddDraw();

        record.Draws.Should().Be(1);
        record.MatchesPlayed.Should().Be(1);
        record.Points.Should().Be(1);
    }

    [Fact]
    public void Should_IncrementLossesAndMatchesPlayed_When_AddLoss()
    {
        var record = new TeamRecord("Allegoric Alaskans");

        record.AddLoss();

        record.Losses.Should().Be(1);
        record.MatchesPlayed.Should().Be(1);
        record.Points.Should().Be(0);
    }

    [Fact]
    public void Should_AccumulatePoints_When_MultipleResultsAreAdded()
    {
        var record = new TeamRecord("Allegoric Alaskans");

        record.AddWin();
        record.AddWin();
        record.AddDraw();
        record.AddLoss();

        record.MatchesPlayed.Should().Be(4);
        record.Points.Should().Be(7);
    }
}
