using MediatR;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands
{
    public class StreamerCommand : IRequest<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}