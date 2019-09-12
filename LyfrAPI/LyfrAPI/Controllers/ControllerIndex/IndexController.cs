using LyfrAPI.Emails.Functions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public IndexController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public string Index()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;

            return "Index"+"\n"+ webRootPath;
        }
    }
}