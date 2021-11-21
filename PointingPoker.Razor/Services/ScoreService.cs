namespace PointingPoker.Razor.Services;

public class ScoreService
{
    public event EventHandler<ScoreEventArgs>? ScoreAdded;

    public event EventHandler<ScoreEventArgs>? ScoreUpdated;

    public void Add(ScoreEventArgs score)
    {
        this.ScoreAdded?.Invoke(this, score);
    }

    public void Update(ScoreEventArgs score)
    {
        this.ScoreUpdated?.Invoke(this, score);
    }
}
