using AutoMapper;
using BoardsManager.Users.Application.Services;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.DTO;
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

            //Act
            bool actual = await sut.AddUserToProjectAsync(projectId, userId);

            //Assert
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

            //Act
            bool actual = await sut.AddUserToProjectAsync(projectId, userId);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task AddUserToProjectAsync_ShouldReturnFalse_IfUserNotFound()
        {
            //Arrange
            string userId = "userId";
            string projectId = "projectId";
            userRepository.GetUserByIdAsync(userId).Returns((User?)null);

            //Act
            bool actual = await sut.AddUserToProjectAsync(projectId, userId);

            //Assert
            await userRepository.DidNotReceive().UpdateUserAsync(Arg.Any<User>());
            Assert.IsFalse(actual);
        }
      
        [TestMethod]
        public async Task CreateUserAsync_ShouldReturnFalse_IfUserNotCreated()
        {
            //Arrange

            //Act
            bool actual = await CreateUser(false);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task CreateUserAsync_ShouldReturnTrue_IfUserCreated()
        {
            //Arrange

            //Act
            bool actual = await CreateUser(true);

            //Assert
            Assert.IsTrue(actual);
        }

        private Task<bool> CreateUser(bool isCreated)
        {
            //Arrange
            UserDTO userDTO = new UserDTO();
            User user = new User();
            mapper.Map<User>(userDTO).Returns(user);
            userRepository.AddUserAsync(user).Returns(isCreated);

            //Act
            return sut.CreateUserAsync(userDTO);
        }

        [TestMethod]
        public async Task ChangeUserPasswordAsync_ShouldReturnFalse_IfUserNotFound()
        {
            //Arrange
            string userId = "userId";
            string currentPassword = "123";
            string newPassword = "12345";
            userRepository.GetUserByIdAsync(userId).Returns((User?)null);

            //Act
            bool actual = await sut.ChangeUserPasswordAsync(userId, currentPassword, newPassword);

            //Assert
            await userRepository.DidNotReceive().UpdateUserAsync(Arg.Any<User>());
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task ChangeUserPasswordAsync_ShouldReturnFalse_IfUserFoundButCantBeUpdated()
        {
            //Arrange
            string userId = "userId";
            string currentPassword = "123";
            string newPassword = "12345";
            User user = new User();
            userRepository.GetUserByIdAsync(userId).Returns(user);
            userRepository.ChangePasswordAsync(user, currentPassword, newPassword).Returns(false);

            //Act
            bool actual = await sut.ChangeUserPasswordAsync(userId, currentPassword, newPassword);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task ChangeUserPasswordAsync_ShouldReturnTrue_IfUserFoundAndUpdated()
        {
            //Arrange
            string userId = "userId";
            string currentPassword = "123";
            string newPassword = "12345";
            User user = new User();
            userRepository.GetUserByIdAsync(userId).Returns(user);
            userRepository.ChangePasswordAsync(user, currentPassword, newPassword).Returns(true);

            //Act
            bool actual = await sut.ChangeUserPasswordAsync(userId, currentPassword, newPassword);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task UpdateUserAsync_ShouldReturnFalse_IfUserNotFound()
        {
            //Arrange
            UserDTO userDTO = new UserDTO()
            {
                Id = "id"
            };
            userRepository.GetUserByIdAsync(userDTO.Id).Returns((User?)null);

            //Act
            bool actual = await sut.UpdateUserAsync(userDTO);

            //Assert
            Assert.IsFalse(actual);
            await userRepository.DidNotReceive().UpdateUserAsync(Arg.Any<User>());
        }

        [TestMethod]
        public async Task UpdateUserAsync_ShouldReturnFalse_IfUserFoundButCantBeUpdated()
        {
            //Arrange
            UserDTO userDTO = new UserDTO()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Id = "id"
            };
            User user = new User();
            userRepository.GetUserByIdAsync(userDTO.Id).Returns(user);
            userRepository.UpdateUserAsync(Arg.Is<User>(u =>
                u.Equals(user) && u.FirstName == userDTO.FirstName && u.LastName == userDTO.LastName
            )).Returns(false);

            //Act
            bool actual = await sut.UpdateUserAsync(userDTO);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public async Task UpdateUserAsync_ShouldReturnTrue_IfUserFoundAndUpdated()
        {
            //Arrange
            UserDTO userDTO = new UserDTO()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Id = "id"
            };
            User user = new User();
            userRepository.GetUserByIdAsync(userDTO.Id).Returns(user);
            userRepository.UpdateUserAsync(Arg.Is<User>(u => 
                u.Equals(user) && u.FirstName == userDTO.FirstName && u.LastName == userDTO.LastName
            )).Returns(true);

            //Act
            bool actual = await sut.UpdateUserAsync(userDTO);

            //Assert
            Assert.IsTrue(actual);
        }
    }
}