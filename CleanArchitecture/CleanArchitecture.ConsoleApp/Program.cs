﻿// See https://aka.ms/new-console-template for more information
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


StreamerDbContext dbContext = new();
await QueryFilter();
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

    var streamingNombre = Console.ReadLine();
    //var streamers = await dbContext!.Streamers!.Where(x => x.Nombre  == streamingNombre).ToListAsync();
    var streamers = await dbContext!.Streamers!.Where(x => x.Nombre!.Equals(streamingNombre)).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{ streamer.Id } - { streamer.Nombre }");
    }

    //var streamerPartialResult = await dbContext!.Streamers!.Where(x => x.Nombre!.Contains(streamingNombre)).ToListAsync();
    var streamerPartialResult = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Nombre!, $"%{streamingNombre}%")).ToListAsync();

    foreach (var streamer in streamerPartialResult)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}