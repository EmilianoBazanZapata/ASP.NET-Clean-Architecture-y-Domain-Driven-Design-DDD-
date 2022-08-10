using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionBuilder) 
        {
            optionBuilder.UseSqlServer("Data Source=DESKTOP-UHFA848\\SQLEXPRESS;Initial Catalog=StreamerDB;Integrated Security=True");
        }



        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
    }
}
