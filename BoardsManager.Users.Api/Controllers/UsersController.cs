using Microsoft.AspNetCore.Mvc;

namespace BoardsManager.Users.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}