using Microsoft.AspNetCore.Mvc;

namespace dev_api.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "CI/CD is working!",
                buildNumber = Environment.GetEnvironmentVariable("BUILD_NUMBER") ?? "local",
                gitCommit = Environment.GetEnvironmentVariable("GIT_COMMIT") ?? "unknown",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
