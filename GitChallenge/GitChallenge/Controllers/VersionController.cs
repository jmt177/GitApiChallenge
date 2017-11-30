using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GitChallenge.Controllers
{
    public class VersionController : Controller
    {
        [HttpGet]
        [Route("api/v1/VersionController")]
        public IActionResult Index()
        {
            return new ObjectResult("GitChallenge Version 1.0 available.");
        }
    }
}