using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Infrastructure;
using CleanArchitecture.Applicattion.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Applicattion.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Applicattion.Mappings;
using CleanArchitecture.Applicattion.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Applicattion.UnitTests.Features.Streamers.DeleteStreamer
{
    public class DeleteStreammerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ILogger<DeleteStreamerCommandHandler>> _logger;

        public DeleteStreammerCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _emailService = new Mock<IEmailService>();

            _logger = new Mock<ILogger<DeleteStreamerCommandHandler>>();

            MockStreramerRepository.AddDataStreamerRepository(_unitOfWork.Object
                                                                         .StreamerDbContext);
        }

        [Fact]
        public async Task UpdateStreammerCommand_InputStreammer_ReturnsUnit()
        {
            var streamerInput = new DeleteStreamerCommand
            {
                Id = 8001
            };

            var handler = new DeleteStreamerCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
