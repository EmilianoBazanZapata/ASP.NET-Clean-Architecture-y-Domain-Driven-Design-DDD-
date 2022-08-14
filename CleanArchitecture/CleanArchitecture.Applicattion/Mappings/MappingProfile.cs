﻿using AutoMapper;
using CleanArchitecture.Applicattion.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Applicattion.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Applicattion.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();
            CreateMap<CreateStreamerCommand, Streamer>();
        }
    }
}
