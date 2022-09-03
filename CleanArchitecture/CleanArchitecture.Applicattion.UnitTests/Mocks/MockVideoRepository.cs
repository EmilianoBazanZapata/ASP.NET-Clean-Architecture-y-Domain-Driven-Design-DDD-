using AutoFixture;
using CleanArchitecture.Applicattion.Contracts.Persistence;
using CleanArchitecture.Domain;
using Moq;

namespace CleanArchitecture.Applicattion.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<IVideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();

            var videos = fixture.CreateMany<Video>().ToList();

            var mockRepository = new Mock<IVideoRepository>();

            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(videos);

            return mockRepository;
        }
    }
}