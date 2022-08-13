// See https://aka.ms/new-console-template for more information
using CleanArchitecture.Data;
using CleanArchitecture.Domain;

Console.WriteLine("Hello, World!");


StreamerDbContext dbContext = new();

Streamer streamer = new()
{
    Nombre = "Amazon Prime",
    Url = "https://www.amazonprime.com"
};

await dbContext!.Streamers!.AddAsync(streamer);
dbContext!.SaveChanges();


var movies = new List<Video> 
{
    new Video
    {
        Nombre = "Mad Max",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Mad Max 2",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Batman",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "Crepusculo",
        StreamerId = streamer.Id
    }
    ,
    new Video
    {
        Nombre = "Citizen Kane",
        StreamerId = streamer.Id
    }
};

await dbContext.AddRangeAsync(movies);
await dbContext.SaveChangesAsync();
