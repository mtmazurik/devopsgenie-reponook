using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using DOG.RepoNook.HelperClasses;
using DOG.RepoNook.Models;
using DOG.RepoNook.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MongoDB.Bson;
using Microsoft.Extensions.Hosting;

namespace DOG.RepoNook.Controllers
{
    [Route("/admin")]
    public class AdminController : Controller
    {
        [HttpPut("kill")]   // Kills the main thread, effectively shutting down the repo-nook-svc
        public IActionResult Kill([FromServices]IHostApplicationLifetime applicationLifetime)
        {
            applicationLifetime.StopApplication();
            return Ok("Main app thread killed.");
        }
        [HttpGet("ping")]   // ping
        public IActionResult GetPing()
        {
            return Ok("200 OK");
        }
        [HttpGet("version")]   // service version (from compiled assembly version)
        public IActionResult GetVersion()
        {
            string version = typeof(Startup).Assembly.GetName().Version.ToString();
            return Ok(version);
        }
    }
}
