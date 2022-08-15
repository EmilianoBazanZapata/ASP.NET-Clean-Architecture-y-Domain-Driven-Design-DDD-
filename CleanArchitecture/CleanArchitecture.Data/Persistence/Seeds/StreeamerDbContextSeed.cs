using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence.Seeds
{
    public class StreeamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context,
                                           ILogger<StreeamerDbContextSeed> logger)
        {
            if (!context!.Streamers!.Any())
            {
                context!.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context!.SaveChangeAsync();
                logger.LogInformation("Estamos insertando nuevos Records asl db {context}", typeof(StreamerDbContext).nam);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer
                {
                    CreatedBy = "Emiliano",
                    Nombre = "Bazan HBP",
                    Url = "http://www.hbp.com"
                },
                new Streamer
                {
                    CreatedBy = "Emiliano",
                    Nombre = "Amazon VIP",
                    Url = "http://www.amazonvip.com"
                }
            };
        }
    }
}
