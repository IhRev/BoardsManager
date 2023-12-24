using Microsoft.AspNetCore.Mvc;

namespace BoardsManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        [HttpGet("cards")]
        public async Task<ActionResult> Get([FromQuery]int projectId)
        {
            await Task.Delay(100);
            return Ok();
        }
    }
}