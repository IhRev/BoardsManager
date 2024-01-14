using BoardsManager.Users.Api.Controllers;
using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Diagnostics.CodeAnalysis;

namespace BoardsManager.Users.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        [AllowNull]
        private UsersController sut;
        [AllowNull]
        private ILogger logger;
        [AllowNull]
        private IUserQueryService userQueryService;
        [AllowNull]
        private IUserRegistrationService userRegistrationService;

        [TestInitialize]
        public void Setup()
        {
            logger = Substitute.For<ILogger>();
            userQueryService = Substitute.For<IUserQueryService>();
            userRegistrationService = Substitute.For<IUserRegistrationService>();
            sut = new UsersController(userQueryService, userRegistrationService, logger);
        }

        [TestMethod]
        public void GetUsers_ShouldReturnOkActionReslt_WithUsers()
        {
            //Arrange
            string projId = "projId";
            List<UserDTO> expected = new List<UserDTO>();
            userQueryService.GetUsersByProjectId(projId).Returns(expected);

            //Act
            ActionResult<IEnumerable<UserDTO>> actual = sut.GetUsers(projId);

            //Assert
            AssertOkObjectResult(expected, actual);
        }

        [TestMethod]
        public void GetUsers_ShouldReturnStatusCode500_IfExceptionWasThrown()
        {
            //Arrange
            string projId = "projId";
            userQueryService.GetUsersByProjectId(projId).Throws<Exception>();

            //Act
            ActionResult<IEnumerable<UserDTO>> actual = sut.GetUsers(projId);

            //Assert
            CheckInternalServerError(actual);
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnOkResult_WithUser()
        {
            //Arrange
            string userId = "userId";
            UserDTO expected = new UserDTO();
            userQueryService.GetUserByIdAsync(userId).Returns(expected);

            //Act
            ActionResult<UserDTO> actual = await sut.GetUser(userId);

            //Assert
            AssertOkObjectResult(expected, actual);
        } 

        [TestMethod]
        public async Task GetUser_ShouldReturnNotFountResult_IfUserIsNull()
        {
            //Arrange
            string userId = "userId";
            userQueryService.GetUserByIdAsync(userId).Returns((UserDTO?)null);

            //Act
            ActionResult<UserDTO> actual = await sut.GetUser(userId);

            //Assert
            Assert.IsInstanceOfType<NotFoundResult>(actual.Result);
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnStatusCode500_IfExceptionWasThrown()
        {
            //Arrange
            string userId = "userId";
            userQueryService.GetUserByIdAsync(userId).ThrowsAsync<Exception>();

            //Act
            ActionResult<UserDTO> actual = await sut.GetUser(userId);

            //Assert
            CheckInternalServerError(actual);
        }

        private void AssertOkObjectResult<T>(T expected, ActionResult<T> actual)
        {
            Assert.IsInstanceOfType<OkObjectResult>(actual.Result);
            OkObjectResult? result = actual.Result as OkObjectResult;
            Assert.IsInstanceOfType<T>(result?.Value);
            T model = (T)result.Value;
            model.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task AddUser_ShouldReturnCreatedResult_IfUserWasCraeted()
        {
            //Arrange
            UserDTO user = new UserDTO();
            userRegistrationService.CreateUserAsync(user).Returns(true);

            //Act
            ActionResult actual = await sut.AddUser(user);

            //Assert
            Assert.IsInstanceOfType<CreatedResult>(actual);
        }

        [TestMethod]
        public async Task AddUser_ShouldReturnUnprocessableEntityResult_IfUserWasntCreated()
        {
            //Arrange
            UserDTO user = new UserDTO();
            userRegistrationService.CreateUserAsync(user).Returns(false);

            //Act
            ActionResult actual = await sut.AddUser(user);

            //Assert
            Assert.IsInstanceOfType<UnprocessableEntityResult>(actual);
        }

        [TestMethod]
        public async Task AddUser_ShouldReturnStatusCode500_IfExceptionWasThrown()
        {
            //Arrange
            UserDTO user = new UserDTO();
            userRegistrationService.CreateUserAsync(user).ThrowsAsync<Exception>();

            //Act
            ActionResult actual = await sut.AddUser(user);

            //Assert
            CheckInternalServerError(actual);
        }

        [TestMethod]
        public async Task AssignUserToProject_ShouldReturnOkResult_IfUserWasAssigned()
        {
            //Arrange
            string userId = "userId";
            string projId = "projId";
            userRegistrationService.AddUserToProjectAsync(projId, userId).Returns(true);

            //Act
            ActionResult actual = await sut.AssignUserToProject(projId, userId);

            //Assert
            Assert.IsInstanceOfType<OkResult>(actual);
        }

        [TestMethod]
        public async Task AssignUserToProject_ShouldReturnUnprocessableEntityResult_IfUserWasntAssigned()
        {
            //Arrange
            string userId = "userId";
            string projId = "projId";
            userRegistrationService.AddUserToProjectAsync(projId, userId).Returns(false);

            //Act
            ActionResult actual = await sut.AssignUserToProject(projId, userId);

            //Assert
            Assert.IsInstanceOfType<UnprocessableEntityResult>(actual);
        }

        [TestMethod]
        public async Task AssignUserToProject_ShouldReturnStatusCode500_IfExceptionWasThrown()
        {
            //Arrange
            string userId = "userId";
            string projId = "projId";
            userRegistrationService.AddUserToProjectAsync(projId, userId).ThrowsAsync<Exception>();

            //Act
            ActionResult actual = await sut.AssignUserToProject(projId, userId);

            //Assert
            CheckInternalServerError(actual);
        } 
        
        [TestMethod]
        public async Task ChangeUserPassword_ShouldReturnOkResult_IfPaaswordWasChanged()
        {
            //Arrange
            string userId = "userId";
            string currentPassword = "1234";
            string newPassword = "12345";
            userRegistrationService.ChangeUserPasswordAsync(userId, currentPassword, newPassword).Returns(true);

            //Act
            ActionResult actual = await sut.ChangeUserPassword(userId, currentPassword, newPassword);

            //Assert
            Assert.IsInstanceOfType<OkResult>(actual);
        }

        [TestMethod]
        public async Task ChangeUserPassword_ShouldReturnUnprocessableEntityResult_IfPaaswordWasntChanged()
        {
            //Arrange
            string userId = "userId";
            string currentPassword = "1234";
            string newPassword = "12345";
            userRegistrationService.ChangeUserPasswordAsync(userId, currentPassword, newPassword).Returns(false);

            //Act
            ActionResult actual = await sut.ChangeUserPassword(userId, currentPassword, newPassword);

            //Assert
            Assert.IsInstanceOfType<UnprocessableEntityResult>(actual);
        }

        [TestMethod]
        public async Task ChangeUserPassword_ShouldReturnStatusCode500_IfExceptionWasThrown()
        {
            //Arrange
            string userId = "userId";
            string currentPassword = "1234";
            string newPassword = "12345";
            userRegistrationService.ChangeUserPasswordAsync(userId, currentPassword, newPassword).ThrowsAsync<Exception>();

            //Act
            ActionResult actual = await sut.ChangeUserPassword(userId, currentPassword, newPassword);

            //Assert
            CheckInternalServerError(actual);
        }
        
        [TestMethod]
        public async Task UpdateUser_ShouldReturnOkResult_IfUserWasUpdated()
        {
            //Arrange
            UserDTO user = new UserDTO();
            userRegistrationService.UpdateUserAsync(user).Returns(true);

            //Act
            ActionResult actual = await sut.UpdateUser(user);

            //Assert
            Assert.IsInstanceOfType<OkResult>(actual);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldReturnUnprocessableEntityResult_IfUserWasntUpdated()
        {
            //Arrange
            UserDTO user = new UserDTO();
            userRegistrationService.UpdateUserAsync(user).Returns(false);

            //Act
            ActionResult actual = await sut.UpdateUser(user);

            //Assert
            Assert.IsInstanceOfType<UnprocessableEntityResult>(actual);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldReturnStatusCode500_IfExceptionWasThrown()
        {
            //Arrange
            UserDTO user = new UserDTO();
            userRegistrationService.UpdateUserAsync(user).ThrowsAsync<Exception>();

            //Act
            ActionResult actual = await sut.UpdateUser(user);

            //Assert
            CheckInternalServerError(actual);
        }

        private void CheckInternalServerError(ActionResult actual)
        {
            CheckLogErrorCalled();
            Assert.IsInstanceOfType<StatusCodeResult>(actual);
            StatusCodeResult? statusCodeResult = actual as StatusCodeResult;
            Assert.AreEqual(500, statusCodeResult?.StatusCode);
        }

        private void CheckInternalServerError<T>(ActionResult<T> actual)
        {
            CheckLogErrorCalled();
            Assert.IsInstanceOfType<StatusCodeResult>(actual.Result);
            StatusCodeResult? statusCodeResult = actual.Result as StatusCodeResult;
            Assert.AreEqual(500, statusCodeResult?.StatusCode);
        }

        private void CheckLogErrorCalled() => logger.Received(1);
    }
}