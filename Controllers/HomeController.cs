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
            var baseDir = AppContext.BaseDirectory;
            var versionFilePath = Path.Combine(baseDir, "version.json");

            return Ok(new
            {
                message = "CI/CD is working!",
                buildNumber = versionInfo?.BuildNumber ?? "local",
                gitCommit = versionInfo?.GitCommit ?? "unknown",
                builtAt = versionInfo?.BuiltAt ?? "unknown",
                timestamp = DateTime.UtcNow,
                // Debug info:
                debug = new
                {
                    baseDirectory = baseDir,
                    versionFilePath = versionFilePath,
                    fileExists = System.IO.File.Exists(versionFilePath),
                    filesInBaseDir = Directory.Exists(baseDir)
                        ? Directory.GetFiles(baseDir).Select(f => Path.GetFileName(f)).ToArray()
                        : new string[] { "directory not found" }
                }
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

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    return JsonSerializer.Deserialize<VersionInfo>(json, options);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading version file: {ex.Message}");
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