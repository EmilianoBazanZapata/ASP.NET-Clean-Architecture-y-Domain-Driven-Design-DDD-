// See https://aka.ms/new-console-template for more information
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


StreamerDbContext dbContext = new();
//await QueryFilter();
//await QueryMethods();
//await QueryLinq();
//await TrackinAndNotTracking();
//await AddNewStreamerWithVieo();
//await AddNewVideoWithVideo();
//await AddNewDirectorWithVideo();
await MultipleEntitiesQuery();

Console.WriteLine("Presione Cualquier tecla para continuar");
Console.ReadKey();

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

async Task TrackinAndNotTracking()
{
    var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);

    //no se podra actualizar el objeto ya que no quedará almacenado en memoria
    var streamerWithNotTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTracking.Nombre = "Netflix Super";
    streamerWithNotTracking.Nombre = "Amazon Plus";

    await dbContext!.SaveChangesAsync();
}

async Task AddNewStreamerWithVieo()
{
    var pantaya = new Streamer
    {
        Nombre = "Pantaya"
    };

    var hungerGames = new Video
    {
        Nombre = "Hunger Games",
        Streamer = pantaya,
    };

    await dbContext.AddAsync(hungerGames);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVieoId()
{
    var batmanForever = new Video
    {
        Nombre = "batman forever",
        StreamerId = 3,
    };

    await dbContext.AddAsync(batmanForever);
    await dbContext.SaveChangesAsync();
}

async Task AddNewVideoWithVideo()
{
    var actor = new Actor
    {
        Nombre = "Brad",
        Apellido = "Pitt"
    };

    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideosActor
    {
        ActorId = actor.Id,
        VideoId = 6
    };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}

async Task AddNewDirectorWithVideo()
{
    var director = new Director
    {
        Nombre = "Lorenzo",
        Apellido = "Basteri",
        VideoId = 6
    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();
}

async Task MultipleEntitiesQuery()
{
    //var videoWithActores = await dbContext!.Videos!.Include(v => v.Actores).FirstOrDefaultAsync(v => v.Id == 6);

    //var actor = await dbContext!.Actores!.Select(a => a.Nombre).ToListAsync();

    var videoWithDirector = await dbContext!.Videos!
                                            .Where(v => v.Director != null)
                                            .Include(v => v.Director)
                                            .Select(v => new
                                            {
                                                Director_Nombre_Completo = $"{v.Director.Nombre} {v.Director.Apellido}",
                                                Pelicula = v.Nombre
                                            }).ToListAsync();

    foreach (var movie in videoWithDirector)
    {
        Console.WriteLine($"{movie.Pelicula} - {movie.Director_Nombre_Completo}");
    }
}