namespace PointingPoker.API.Data;

public class GameSession
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int SessionId { get; set; }

    public DateTime StartTime { get; set; }

    public ICollection<Point> PointVotes { get; init;} = new List<Point>();

    public ICollection<Player> Players { get; init; } = new List<Player>();

    public decimal AveragePoints { get; set; }

    public string? StoryDescription { get; set; }
}
