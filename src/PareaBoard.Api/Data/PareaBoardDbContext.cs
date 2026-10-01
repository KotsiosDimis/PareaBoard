using PareaBoard.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PareaBoard.Api.Data
{
    public class PareaBoardDbContext(DbContextOptions<PareaBoardDbContext> options)
    : DbContext(options)
    {
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<GamePlayer> GamePlayers => Set<GamePlayer>();
        public DbSet<Round> Rounds => Set<Round>();
        public DbSet<RoundScore> RoundScores => Set<RoundScore>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<GamePlayer>()
                .HasIndex(gp => new { gp.GameId, gp.PlayerId })
                .IsUnique();                    // a player can't join the same game twice

            modelBuilder.Entity<Round>()
                .HasIndex(r => new { r.GameId, r.Number })
                .IsUnique();

            modelBuilder.Entity<RoundScore>()
                .HasIndex(s => new { s.RoundId, s.GamePlayerId })
                .IsUnique();

            modelBuilder.Entity<Player>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<GamePlayer>()
                .HasOne(gp => gp.Player)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(gp => gp.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
                            
        }
    }
}
