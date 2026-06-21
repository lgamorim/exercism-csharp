using System;
using FluentAssertions;
using Xunit;

namespace Exercism.Tournament.UnitTests;

public class TournamentTests
{
    private const string Header = "Team                           | MP |  W |  D |  L |  P";

    private readonly ITestOutputHelper testOutputHelper;

    public TournamentTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Should_ShowOnlyTheHeader_When_NoMatchesAreRecorded()
    {
        var tournament = new Tournament();

        tournament.ToString().Should().Be(Header);
    }

    [Fact]
    public void Should_ThrowArgumentOutOfRangeException_When_OutcomeIsUnknown()
    {
        var tournament = new Tournament();

        var act = () => tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", (MatchOutcome)99);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_RegisterTeam_When_ItOnlyEverAppearsAsTheAwayTeam()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  1 |  1 |  0 |  0 |  3\n" +
            "Blithering Badgers             |  1 |  0 |  0 |  1 |  0";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_AwardThreePointsToWinnerAndZeroToLoser_When_HomeTeamWins()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  1 |  1 |  0 |  0 |  3\n" +
            "Blithering Badgers             |  1 |  0 |  0 |  1 |  0";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_AwardThreePointsToOpponentAndZeroToHomeTeam_When_HomeTeamLoses()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Blithering Badgers", "Allegoric Alaskans", MatchOutcome.Loss);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  1 |  1 |  0 |  0 |  3\n" +
            "Blithering Badgers             |  1 |  0 |  0 |  1 |  0";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_AwardOnePointEachToBothTeams_When_MatchIsADraw()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Draw);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  1 |  0 |  1 |  0 |  1\n" +
            "Blithering Badgers             |  1 |  0 |  1 |  0 |  1";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_AccumulateMatchesPlayedAndPoints_When_TheSameTeamsPlayMoreThanOnce()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  2 |  2 |  0 |  0 |  6\n" +
            "Blithering Badgers             |  2 |  0 |  0 |  2 |  0";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_TieTeamsByPoints_When_BothHaveOneWinAndOneLoss()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Loss);
        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  2 |  1 |  0 |  1 |  3\n" +
            "Blithering Badgers             |  2 |  1 |  0 |  1 |  3";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_RankMoreThanTwoTeams_When_ThreeTeamsHavePlayed()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Blithering Badgers", "Courageous Californians", MatchOutcome.Win);
        tournament.RecordMatch("Courageous Californians", "Allegoric Alaskans", MatchOutcome.Loss);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  2 |  2 |  0 |  0 |  6\n" +
            "Blithering Badgers             |  2 |  1 |  0 |  1 |  3\n" +
            "Courageous Californians        |  2 |  0 |  0 |  2 |  0";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_OrderByPointsDescending_When_TypicalCompetitionHasBeenPlayed()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Devastating Donkeys", "Courageous Californians", MatchOutcome.Draw);
        tournament.RecordMatch("Devastating Donkeys", "Allegoric Alaskans", MatchOutcome.Win);
        tournament.RecordMatch("Courageous Californians", "Blithering Badgers", MatchOutcome.Loss);
        tournament.RecordMatch("Blithering Badgers", "Devastating Donkeys", MatchOutcome.Loss);
        tournament.RecordMatch("Allegoric Alaskans", "Courageous Californians", MatchOutcome.Win);

        var expected =
            Header + "\n" +
            "Devastating Donkeys            |  3 |  2 |  1 |  0 |  7\n" +
            "Allegoric Alaskans             |  3 |  2 |  0 |  1 |  6\n" +
            "Blithering Badgers             |  3 |  1 |  0 |  2 |  3\n" +
            "Courageous Californians        |  3 |  0 |  1 |  2 |  1";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_OnlyCountPlayedMatches_When_NotAllTeamsHavePlayedEachOther()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Loss);
        tournament.RecordMatch("Devastating Donkeys", "Allegoric Alaskans", MatchOutcome.Loss);
        tournament.RecordMatch("Courageous Californians", "Blithering Badgers", MatchOutcome.Draw);
        tournament.RecordMatch("Allegoric Alaskans", "Courageous Californians", MatchOutcome.Win);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  3 |  2 |  0 |  1 |  6\n" +
            "Blithering Badgers             |  2 |  1 |  1 |  0 |  4\n" +
            "Courageous Californians        |  2 |  0 |  1 |  1 |  1\n" +
            "Devastating Donkeys            |  1 |  0 |  0 |  1 |  0";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_BreakTiesAlphabetically_When_TeamsHaveTheSamePoints()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Courageous Californians", "Devastating Donkeys", MatchOutcome.Win);
        tournament.RecordMatch("Allegoric Alaskans", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Devastating Donkeys", "Allegoric Alaskans", MatchOutcome.Loss);
        tournament.RecordMatch("Courageous Californians", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Blithering Badgers", "Devastating Donkeys", MatchOutcome.Draw);
        tournament.RecordMatch("Allegoric Alaskans", "Courageous Californians", MatchOutcome.Draw);

        var expected =
            Header + "\n" +
            "Allegoric Alaskans             |  3 |  2 |  1 |  0 |  7\n" +
            "Courageous Californians        |  3 |  2 |  1 |  0 |  7\n" +
            "Blithering Badgers             |  3 |  0 |  1 |  2 |  1\n" +
            "Devastating Donkeys            |  3 |  0 |  1 |  2 |  1";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    [Fact]
    public void Should_SortByPointsNumerically_When_PointsReachDoubleDigits()
    {
        var tournament = new Tournament();

        tournament.RecordMatch("Devastating Donkeys", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Devastating Donkeys", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Devastating Donkeys", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Devastating Donkeys", "Blithering Badgers", MatchOutcome.Win);
        tournament.RecordMatch("Blithering Badgers", "Devastating Donkeys", MatchOutcome.Win);

        var expected =
            Header + "\n" +
            "Devastating Donkeys            |  5 |  4 |  0 |  1 | 12\n" +
            "Blithering Badgers             |  5 |  1 |  0 |  4 |  3";
        Print(tournament);
        tournament.ToString().Should().Be(expected);
    }

    private void Print(Tournament tournament)
    {
        testOutputHelper.WriteLine(tournament.ToString());
    }
}
