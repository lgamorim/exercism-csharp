using System.Text;

namespace Exercism.Tournament;

public class Tournament
{
    private const string Header = "Team                           | MP |  W |  D |  L |  P";

    private readonly Dictionary<string, TeamRecord> teamRecords = new();

    public void RecordMatch(string homeTeam, string awayTeam, MatchOutcome outcome)
    {
        var home = GetOrAddTeam(homeTeam);
        var away = GetOrAddTeam(awayTeam);

        switch (outcome)
        {
            case MatchOutcome.Win:
                home.AddWin();
                away.AddLoss();
                break;
            case MatchOutcome.Draw:
                home.AddDraw();
                away.AddDraw();
                break;
            case MatchOutcome.Loss:
                home.AddLoss();
                away.AddWin();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown match outcome.");
        }
    }

    public override string ToString()
    {
        var table = new StringBuilder(Header);
        foreach (var team in RankedTeams())
        {
            table.Append('\n').Append(FormatRow(team));
        }

        return table.ToString();
    }

    private IEnumerable<TeamRecord> RankedTeams()
    {
        return teamRecords.Values
            .OrderByDescending(team => team.Points)
            .ThenBy(team => team.Name, StringComparer.Ordinal);
    }

    private TeamRecord GetOrAddTeam(string name)
    {
        if (!teamRecords.TryGetValue(name, out var record))
        {
            record = new TeamRecord(name);
            teamRecords[name] = record;
        }

        return record;
    }

    private static string FormatRow(TeamRecord team)
    {
        return $"{team.Name,-31}| {team.MatchesPlayed,2} | {team.Wins,2} | {team.Draws,2} | {team.Losses,2} | {team.Points,2}";
    }
}
