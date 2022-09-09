using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Infrastructure;
using CleanArchitecture.Applicattion.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Applicattion.Mappings;
using CleanArchitecture.Applicattion.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Applicattion.UnitTests.Features.Streamers.CreateStreamer
{
    public class CreateStreamerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ILogger<CreateStreamerCommandHandler>> _logger;

        public CreateStreamerCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _emailService = new Mock<IEmailService>();

            _logger = new Mock<ILogger<CreateStreamerCommandHandler>>();

            MockStreramerRepository.AddDataStreamerRepository(_unitOfWork.Object
                                                                         .StreamerDbContext);
        }

        [Fact]
        public async Task CreateStreammerCommand_InputStreamer_ReturnsNumber() 
        {
            var streamerInput = new CreateStreamerCommand
            {
                Nombre = "prueba Streameing",
                Url = "www.prueba.com"
            };

            var handler = new CreateStreamerCommandHandler(_unitOfWork.Object,_mapper,_emailService.Object, _logger.Object);

            var result = await handler.Handle(streamerInput,CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}