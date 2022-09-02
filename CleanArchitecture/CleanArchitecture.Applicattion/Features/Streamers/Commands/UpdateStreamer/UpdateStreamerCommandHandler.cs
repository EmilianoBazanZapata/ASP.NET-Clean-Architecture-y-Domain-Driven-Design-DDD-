using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Persistence;
using CleanArchitecture.Applicattion.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateStreamerCommandHandler(IUnitOfWork unitOfWork,
                                            IMapper mapper,
                                            ILogger<UpdateStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            // streamerToUpdate = await _streamerRepository.GetByIdAsync(request.Id);

            var streamerToUpdate = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);

            if (streamerToUpdate is null) 
            {
                _logger.LogError($"no se encontro el streamer id {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));

            //await _streamerRepository.UpdateAsync(streamerToUpdate);

            _unitOfWork.StreamerRepository.UpdateEntity(streamerToUpdate);
            await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer {request.Id}");

            return Unit.Value;
        }
    }
}
