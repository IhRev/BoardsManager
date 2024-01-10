using AutoMapper;
using BoardsManager.Users.Application.Services;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Domain.Entities;
using BoardsManager.Users.Domain.Repositories;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;

namespace BoardsManager.Users.Tests.Services
{
    [TestClass]
    public class UserRegistrationServiceTests
    {
        [AllowNull]
        private IUserRegistrationService sut;
        [AllowNull]
        private IUserRepository userRepository;
        [AllowNull]
        private IMapper mapper;

        [TestInitialize]
        public void Setup()
        {
            userRepository = Substitute.For<IUserRepository>();
            mapper = Substitute.For<IMapper>();
            sut = new UserRegistrationService(userRepository, mapper);
        }

        [TestMethod]
        public async Task AddUserToProjectAsync_ShouldReturnTrue_IfUserFoundAndUpdated()
        {
            //Arrange
            string userId = "userId";
            string projectId = "projectId";
            User user = new User();
            userRepository.GetUserByIdAsync(userId).Returns(user);
            userRepository.UpdateUserAsync(user).Returns(true);

            //Assert
            bool actual = await sut.AddUserToProjectAsync(projectId, userId);

            //Act
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task AddUserToProjectAsync_ShouldReturnFalse_IfUserFoundButCantBeUpdated()
        {
            //Arrange
            string userId = "userId";
            string projectId = "projectId";
            User user = new User();
            userRepository.GetUserByIdAsync(userId).Returns(user);
            userRepository.UpdateUserAsync(user).Returns(false);

            //Assert
            bool actual = await sut.AddUserToProjectAsync(projectId, userId);

            //Act
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task AddUserToProjectAsync_ShouldReturnFalse_IfUserNotFound()
        {
            //Arrange
            string userId = "userId";
            string projectId = "projectId";
            userRepository.GetUserByIdAsync(userId).Returns((User?)null);

            //Assert
            bool actual = await sut.AddUserToProjectAsync(projectId, userId);

            //Act
            await userRepository.DidNotReceive().UpdateUserAsync(Arg.Any<User>());
            Assert.IsFalse(actual);
        }
    }
}