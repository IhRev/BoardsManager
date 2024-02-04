using AutoMapper;
using BoardsManager.Users.Application.Services;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.Dto;
using BoardsManager.Users.Domain.Entities;
using BoardsManager.Users.Domain.Repositories;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;

namespace BoardsManager.Users.Tests.Services
{
    [TestClass]
    public class UserQueryServiceTests
    {
        [AllowNull]
        private UserQueryService sut;
        [AllowNull]
        private IUserRepository userRepository;
        [AllowNull]
        private IMapper mapper;

        [TestInitialize] 
        public void Setup()
        {
            userRepository = Substitute.For<IUserRepository>();
            mapper = Substitute.For<IMapper>();
            sut = new UserQueryService(userRepository, mapper);
        }

        [TestMethod]
        public void GetUsersByProjectId_ShouldReturnCollectionOfUsers()
        {
            //Arrange
            string projectId = "projId";
            IEnumerable<User> users = new List<User>();

            IEnumerable<UserDto> expected = new List<UserDto>();

            userRepository.GetUsersByProjectId(projectId).Returns(users);

            mapper.Map<IEnumerable<UserDto>>(users).Returns(expected);

            //Act
            IEnumerable<UserDto> actual = sut.GetUsersByProjectId(projectId);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ShouldReturnUser_IfFound()
        {
            //Arrange
            string userId = "projId";
            User? user = new();

            UserDto? expected = new();

            userRepository.GetUserByIdAsync(userId).Returns(user);

            mapper.Map<UserDto>(user).Returns(expected);

            //Act
            UserDto? actual = await sut.GetUserByIdAsync(userId);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}