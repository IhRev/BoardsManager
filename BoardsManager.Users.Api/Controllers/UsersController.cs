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

        [HttpGet("project_users")]
        public ActionResult<IAsyncEnumerable<UserDTO>> GetUsers([FromQuery] Guid projectId)
        {
            try
            {
                IAsyncEnumerable<UserDTO> users = userQueryService.GetUsersByProjectId(projectId);
                return Ok(users);
            }
            catch (Exception e)
            {
                return GetInternalServerError(e);
            }
        }

        [HttpPost("add_user")]
        public async Task<ActionResult<Guid>> AddUser([FromBody]UserDTO user)
        {
            try
            {
                Guid newId = await userRegistrationService.RegisterUser(user);
                return Ok(newId);
            }
            catch (Exception e)
            {
                return GetInternalServerError(e);
            }
        }

        [HttpPut("add_user_to_project")]
        public async Task<ActionResult<bool>> AddUserToProject(Guid projectId, Guid userId)
        {
            try
            {
                bool isAssigned = await userRegistrationService.AssignUserToProject(projectId, userId);
                return Ok(isAssigned);
            }
            catch (Exception e)
            {
                return GetInternalServerError(e);
            }
        }

        private StatusCodeResult GetInternalServerError(Exception e)
        {
            logger.LogError(e, "Unhandled exception");
            return StatusCode(500);
        }
    }
}