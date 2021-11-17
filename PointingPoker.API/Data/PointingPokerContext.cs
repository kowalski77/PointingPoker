namespace PointingPoker.API.Data;

public class PointingPokerContext : DbContext
{
    public PointingPokerContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<GameSession> Sessions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.Entity<GameSession>(x => x.OwnsMany(y => y.PointVotes, y => y.Property(w => w.Id)));
    }
}
