using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GitChallenge.Controllers
{
    public class GitHubFollowerController : Controller
    {
        private readonly GitClient gitClient;
        private const int levels = 3;

        public GitHubFollowerController(IConfiguration configuration)
        {
            gitClient = new GitClient(configuration);
        }

        [HttpGet]
        [Route("api/v1/GitHubFollowerController/{user}")]
        public IActionResult Get(string user)
        {
            try
            {
                var followers = gitClient.GetFollowerLevels(user, levels);
                var jsonContent = JsonConvert.SerializeObject(followers, Formatting.Indented);
                return Content(jsonContent, "application/json");
            }
            catch (Exception e)
            {
                return StatusCode(503, e);
            }
        }
    }
}