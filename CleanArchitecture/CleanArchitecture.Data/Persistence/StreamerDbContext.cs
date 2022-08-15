using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "System";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
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
