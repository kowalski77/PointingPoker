using Microsoft.EntityFrameworkCore;

namespace PointingPoker.API.Data;

public class PointingPokerContext : DbContext
{
    public PointingPokerContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<GameSession> Sessions { get; set; } = default!;
    
    public DbSet<Player> Players { get; set; } = default!;
}
