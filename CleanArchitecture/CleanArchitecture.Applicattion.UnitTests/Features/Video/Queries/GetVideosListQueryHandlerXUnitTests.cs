using AutoMapper;
using CleanArchitecture.Applicattion.Contracts.Persistence;
using CleanArchitecture.Applicattion.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Applicattion.Mappings;
using CleanArchitecture.Applicattion.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Applicattion.UnitTests.Features.Video.Queries
{
    public class GetVideosListQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWOrk;

        public GetVideosListQueryHandlerXUnitTests()
        {
            _unitOfWOrk = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            MockVideoRepository.AddDataVideoRepository(_unitOfWOrk.Object.StreamerDbContext);
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            var handler = new GetVideosListQueryHandler(_unitOfWOrk.Object, _mapper);

            var request = new GetVideosListQuery("system");

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<VideosVm>>();

            result.Count.ShouldBe(1);
        }
    }
}