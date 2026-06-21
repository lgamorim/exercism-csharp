namespace Exercism.Tournament;

public class TeamRecord
{
    private const int PointsPerWin = 3;
    private const int PointsPerDraw = 1;

    public TeamRecord(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public int Wins { get; private set; }

    public int Draws { get; private set; }

    public int Losses { get; private set; }

    public int MatchesPlayed => Wins + Draws + Losses;

    public int Points => Wins * PointsPerWin + Draws * PointsPerDraw;

    public void AddWin()
    {
        Wins++;
    }

    public void AddDraw()
    {
        Draws++;
    }

    public void AddLoss()
    {
        Losses++;
    }
}
