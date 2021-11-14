namespace PointingPoker.API.Data;

public class GameSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public int SessionId { get; set; }

    public DateTime StartTime { get; set; }

    public IReadOnlyList<int> PointVotes { get; } = new List<int>();
    
    public ICollection<Player> Players { get; } = new List<Player>();

    public decimal AveragePoints { get; set; }

    public string? StoryDescription { get; set; }
}
