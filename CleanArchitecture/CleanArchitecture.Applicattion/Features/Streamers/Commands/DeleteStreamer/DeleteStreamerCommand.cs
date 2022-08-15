using MediatR;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
