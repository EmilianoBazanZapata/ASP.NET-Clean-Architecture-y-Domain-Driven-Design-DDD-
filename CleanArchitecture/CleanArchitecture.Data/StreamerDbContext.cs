using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Data Source=DESKTOP-UHFA848\\SQLEXPRESS;Initial Catalog=StreamerDB;Integrated Security=True")
                         .LogTo(Console.WriteLine,
                                        new[] { DbLoggerCategory.Database.Command.Name },
                                        LogLevel.Information)
                         .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relacion uno a muchos
            modelBuilder.Entity<Streamer>()
                        .HasMany(s => s.Videos)
                        .WithOne(s => s.Streamer)
                        .HasForeignKey(s => s.StreamerId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

            //relacion muchos a muchos
            modelBuilder.Entity<Video>()
                        .HasMany(v => v.Actores)
                        .WithMany(v => v.Videos)
                        .UsingEntity<VideosActor>(
                              pt => pt.HasKey(e => new
                              {
                                  e.ActorId,
                                  e.VideoId
                              })
                        );
        }


        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }

        public DbSet<Actor>? Actores { get; set; }

        public DbSet<Director>? Directores { get; set; }
    }
}
