// See https://aka.ms/new-console-template for more information
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


StreamerDbContext dbContext = new();
//await QueryFilter();
//await QueryMethods();
await QueryLinq();

Console.WriteLine("Presione Cualquier tecla para continuar");


//Streamer streamer = new()
//{
//    Nombre = "Amazon Prime",
//    Url = "https://www.amazonprime.com"
//};

//await dbContext!.Streamers!.AddAsync(streamer);
//dbContext!.SaveChanges();


//var movies = new List<Video> 
//{
//    new Video
//    {
//        Nombre = "Mad Max",
//        StreamerId = streamer.Id
//    },
//    new Video
//    {
//        Nombre = "Mad Max 2",
//        StreamerId = streamer.Id
//    },
//    new Video
//    {
//        Nombre = "Batman",
//        StreamerId = streamer.Id
//    },
//    new Video
//    {
//        Nombre = "Crepusculo",
//        StreamerId = streamer.Id
//    }
//    ,
//    new Video
//    {
//        Nombre = "Citizen Kane",
//        StreamerId = streamer.Id
//    }
//};

//await dbContext.AddRangeAsync(movies);
//await dbContext.SaveChangesAsync();


async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una compania de streaming");

    var streamerNombre = Console.ReadLine();
    //var streamers = await dbContext!.Streamers!.Where(x => x.Nombre  == streamingNombre).ToListAsync();
    var streamers = await dbContext!.Streamers!.Where(x => x.Nombre!.Equals(streamerNombre)).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

    //var streamerPartialResult = await dbContext!.Streamers!.Where(x => x.Nombre!.Contains(streamingNombre)).ToListAsync();
    var streamerPartialResult = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Nombre!, $"%{streamerNombre}%")).ToListAsync();

    foreach (var streamer in streamerPartialResult)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task QueryMethods()
{
    var streamer = dbContext.Streamers!;
   
    var firstAsync = await streamer.Where(y => y.Nombre!.Contains("a")).FirstAsync();
    
    var firstOrDefaultAsync = await streamer.Where(y => y.Nombre!.Contains("a")).FirstOrDefaultAsync();
   
    var firstOrDefaultAsync_2 = await streamer.FirstOrDefaultAsync(y => y.Nombre!.Contains("a"));

    //disparará una excepcion al momento de no encontrar un resultado
    var singleAsync = await streamer.Where(y => y.Id == 1).SingleAsync();

    //no disparará una excepcion al momento de no encontrar un resultado
    var singleAsyncOrDefaultAsync = await streamer.Where(y => y.Id == 1).SingleOrDefaultAsync();

    var resultado = await streamer.FindAsync(1);
}

async Task QueryLinq() 
{
    //var streamers = await (from i in dbContext.Streamers
    //                 select i).ToListAsync();
    Console.WriteLine($"Ingrese una compania de streaming");

    var streamerNombre = Console.ReadLine();

    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}