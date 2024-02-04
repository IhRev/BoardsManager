using Microsoft.AspNetCore.Mvc;

namespace BoardsManager.Projects.Api.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectsController(ILogger logger) : ControllerBase
    {
        private readonly ILogger logger = logger;

        [HttpGet("/{projectId}")]
        public ActionResult GetProject([FromRoute]string projectId)
        {
            try
            {
                return Ok(projectId);
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