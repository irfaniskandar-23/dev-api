using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace dev_api.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var versionInfo = GetVersionInfo();

            return Ok(new
            {
                message = "CI/CD is working!",
                buildNumber = versionInfo?.BuildNumber ?? "local",
                gitCommit = versionInfo?.GitCommit ?? "unknown",
                builtAt = versionInfo?.BuiltAt ?? "unknown",
                timestamp = DateTime.UtcNow
            });
        }

        private VersionInfo? GetVersionInfo()
        {
            try
            {
                var versionFile = Path.Combine(AppContext.BaseDirectory, "version.json");
                if (System.IO.File.Exists(versionFile))
                {
                    var json = System.IO.File.ReadAllText(versionFile);
                    return JsonSerializer.Deserialize<VersionInfo>(json);
                }
            }
            catch
            {
                // If file doesn't exist or can't be read, return null
            }

            return null;
        }
    }

    public class VersionInfo
    {
        public string BuildNumber { get; set; } = "";
        public string GitCommit { get; set; } = "";
        public string BuiltAt { get; set; } = "";
    }
}