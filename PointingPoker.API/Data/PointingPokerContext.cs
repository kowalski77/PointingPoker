using Microsoft.EntityFrameworkCore;

namespace PointingPoker.API.Data;

public class PointingPokerContext : DbContext
{
    public PointingPokerContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<GameSession> Sessions { get; set; } = default!;

    public DbSet<Player> Players { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.Entity<Player>(x => x.OwnsOne(y => y.Points, y => y.Property(w => w.Id)));
    }
}
