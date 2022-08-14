using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Infrastructure;
using CleanArchitecture.Applicattion.Contracts.Persistence;
using CleanArchitecture.Applicattion.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands
{
    public class StreamerCommandHandler : IRequestHandler<StreamerCommand, int>
    {
        private readonly IStreamerRepository _StreamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<StreamerCommandHandler> _logger;

        public StreamerCommandHandler(IStreamerRepository streamerRepository, 
                                      IMapper mapper, 
                                      IEmailService emailService, 
                                      ILogger<StreamerCommandHandler> logger)
        {
            _StreamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(StreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);

            var newStreamer = await _StreamerRepository.AddAsync(streamerEntity);

            _logger.LogInformation($"Streamer {newStreamer.Id} fue creado exitosamente");

            await SendEmail(newStreamer);

            return newStreamer.Id;
        }


        //envio de correos
        private async Task SendEmail(Streamer streamer) 
        {
            var email = new Email
            {
                To = "emilianobz546@gmail.com",
                Body = "La compania de Streamer se creo exitosamente",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Errores enviando el email de {streamer.Id}");
            }
        }
    }
}
