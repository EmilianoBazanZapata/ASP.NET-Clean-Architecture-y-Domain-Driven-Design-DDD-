using MediatR;

namespace CleanArchitecture.Applicattion.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQuery : IRequest<List<VideosVm>>
    {
        public string _UserName { get; set; }  = string.Empty;

        public GetVideosListQuery(string userName)
        {
            _UserName = userName ?? throw new ArgumentNullException(nameof(userName));


        }
    }
}
