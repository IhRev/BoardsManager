using BoardsManager.Users.Core.Abstractions;
using BoardsManager.Users.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BoardsManager.Users.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserQueryService userQueryService;
        private readonly IUserRegistrationService userRegistrationService;
        private readonly ILogger logger;

        public UsersController(
            IUserQueryService userQueryService, 
            IUserRegistrationService userRegistrationService,
            ILogger logger)
        {
            this.userQueryService = userQueryService;
            this.userRegistrationService = userRegistrationService;
            this.logger = logger;
        }

        [HttpGet("project_users/{projectId}")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers([FromRoute] string projectId)
        {
            try
            {
                IEnumerable<UserDTO> users = userQueryService.GetUsersByProjectId(projectId);
                return Ok(users);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser([FromRoute] string userId)
        {
            try
            {
                UserDTO? user = await userQueryService.GetUserByIdAsync(userId);
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
        public async Task<ActionResult<bool>> AddUser([FromBody] UserDTO user)
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
        public async Task<ActionResult<bool>> AssignUserToProject([FromQuery]string projectId, [FromQuery]string userId)
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
        public async Task<ActionResult<bool>> ChangeUserPassword([FromQuery] string userId, [FromQuery] string currentPassword, [FromQuery] string newPassword)
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
        public async Task<ActionResult<bool>> UpdateUser([FromBody] UserDTO user)
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