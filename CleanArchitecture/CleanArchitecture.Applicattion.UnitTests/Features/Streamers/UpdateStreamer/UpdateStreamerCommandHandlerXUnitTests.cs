using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Infrastructure;
using CleanArchitecture.Applicattion.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Applicattion.Mappings;
using CleanArchitecture.Applicattion.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Applicattion.UnitTests.Features.Streamers.UpdateStreamer
{
    public class UpdateStreamerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ILogger<UpdateStreamerCommandHandler>> _logger;

        public UpdateStreamerCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _emailService = new Mock<IEmailService>();

            _logger = new Mock<ILogger<UpdateStreamerCommandHandler>>();

            MockStreramerRepository.AddDataStreamerRepository(_unitOfWork.Object
                                                                         .StreamerDbContext);
        }

        [Fact]
        public async Task UpdateStreammerCommand_InputStreammer_ReturnsUnit()
        {
            var streamerInput = new UpdateStreamerCommand
            {
                Id = 8001,
                Nombre = "actualización mimosa",
                Url = "peposo.com"
            };

            var handler = new UpdateStreamerCommandHandler(_unitOfWork.Object,_mapper,_logger.Object);

            var result = await handler.Handle(streamerInput,CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }

    }
}
