using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Persistence;
using CleanArchitecture.Applicattion.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateSteamerCommand>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateStreamerCommandHandler(IStreamerRepository streamerRepository,
                                            IMapper mapper,
                                            ILogger logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateSteamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _streamerRepository.GetByIdAsync(request.Id);

            if (streamerToUpdate is null) 
            {
                _logger.LogError($"no se encontro el streamer id {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _mapper.Map(request, streamerToUpdate, typeof(UpdateSteamerCommand), typeof(Streamer));

            await _streamerRepository.UpdateAsync(streamerToUpdate);

            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer {request.Id}");

            return Unit.Value;
        }
    }
}
