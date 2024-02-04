using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BoardsManager.Users.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController(
        IUserQueryService userQueryService,
        IUserRegistrationService userRegistrationService,
        ILogger logger) : ControllerBase
    {
        [HttpGet("project_users/{projectId}")]
        public ActionResult<IEnumerable<UserDto>> GetUsers([FromRoute] string projectId)
        {
            try
            {
                IEnumerable<UserDto> users = userQueryService.GetUsersByProjectId(projectId);
                return Ok(users);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet("/{userId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] string userId)
        {
            try
            {
                UserDto? user = await userQueryService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUser([FromBody] UserDto user)
        {
            try
            {
                bool created = await userRegistrationService.CreateUserAsync(user);
                if (!created)
                {
                    return UnprocessableEntity();
                }
                return Created();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut("assign_to_project")]
        public async Task<ActionResult> AssignUserToProject([FromQuery]string projectId, [FromQuery]string userId)
        {
            try
            {
                bool added = await userRegistrationService.AddUserToProjectAsync(projectId, userId);
                if (!added)
                {
                    return UnprocessableEntity();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut("change_password")]
        public async Task<ActionResult> ChangeUserPassword([FromQuery] string userId, [FromQuery] string currentPassword, [FromQuery] string newPassword)
        {
            try
            {
                bool changed = await userRegistrationService.ChangeUserPasswordAsync(userId, currentPassword, newPassword);
                if (!changed)
                {
                    return UnprocessableEntity();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto user)
        {
            try
            {
                bool changed = await userRegistrationService.UpdateUserAsync(user);
                if (!changed)
                {
                    return UnprocessableEntity();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private StatusCodeResult InternalServerError(Exception e)
        {
            logger.LogError(e, "Unhandled exception");
            return StatusCode(500);
        }
    }
}