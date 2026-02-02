using Microsoft.AspNetCore.Mvc;

namespace dev_api.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private static readonly string[] value = ["/api/student"];

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "Welcome to fun-demo API!",
                availableEndpoints = value
            });
        }
    }
}
