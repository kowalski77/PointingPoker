namespace PointingPoker.API.Data;

public class Player
{
    public Guid Id { get; set; }
    
    public string Name { get; init; } = default!;
    
    public DateTime TimeJoined { get; set; } = DateTime.UtcNow;

    public int? Point { get; set; }

    public bool IsObserver { get; set; }
}
