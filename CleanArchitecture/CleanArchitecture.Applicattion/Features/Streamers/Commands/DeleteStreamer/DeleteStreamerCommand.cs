using MediatR;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands.DeleteStreamer
{
    internal class DeleteStreamerCommand : IRequest
    {
        public int Id { get; set; }
    }
}
