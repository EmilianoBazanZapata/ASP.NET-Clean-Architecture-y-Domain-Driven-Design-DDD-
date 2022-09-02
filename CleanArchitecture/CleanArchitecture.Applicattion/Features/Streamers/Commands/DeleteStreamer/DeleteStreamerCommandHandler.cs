using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Persistence;
using CleanArchitecture.Applicattion.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork,
                                            IMapper mapper,
                                            ILogger<DeleteStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<Unit> IRequestHandler<DeleteStreamerCommand, Unit>.Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);

            if (streamerToDelete is null) 
            {
                _logger.LogError($"{request.Id} streamer no existe en el sistema");
                throw new NotFoundException(nameof(Streamer),request.Id);
            }

            _unitOfWork.StreamerRepository.DeleteEntity(streamerToDelete);
            await _unitOfWork.Complete();

            _logger.LogInformation($"El {request.Id} streamer fue elminiado con exito");

            return Unit.Value;
        }
    }
}
