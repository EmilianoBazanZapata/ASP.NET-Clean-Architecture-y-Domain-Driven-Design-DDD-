using CleanArchitecture.Domain;

namespace CleanArchitecture.Applicattion.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<Video> GetVideoByName(string nombreVideo);
        Task<IEnumerable<Video>> GetVideoByUserName(string userName);
    }
}
