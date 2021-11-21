namespace PointingPoker.Razor.Services;

public class ScoreEventArgs : EventArgs
{
    public Guid PlayerId { get; init; }

    public string PlayerName { get; init; } = string.Empty;

    public int? Points { get; init; }
}
