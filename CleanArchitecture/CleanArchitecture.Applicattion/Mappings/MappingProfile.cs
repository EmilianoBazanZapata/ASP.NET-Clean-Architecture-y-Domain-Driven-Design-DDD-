using AutoMapper;
using CleanArchitecture.Applicattion.Features.Streamers.Commands;
using CleanArchitecture.Applicattion.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Applicattion.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();
            CreateMap<StreamerCommand, Streamer>();
        }
    }
}
