﻿using CleanArchitecture.Applicattion.Contracts.Persistence;
using Moq;

namespace CleanArchitecture.Applicattion.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockVideoRepository = MockVideoRepository.GetVideoRepository();

            mockUnitOfWork.Setup(r=>r.VideoRepository).Returns(mockVideoRepository.Object);

            return mockUnitOfWork;
        }
    }
}