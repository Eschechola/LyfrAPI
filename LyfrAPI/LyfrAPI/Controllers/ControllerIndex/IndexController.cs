using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        //private readonly IHostingEnvironment _hostingEnvironment;

        //public IndexController(IHostingEnvironment hostingEnvironment)
        //{
            //_hostingEnvironment = hostingEnvironment;
        //}

        public IActionResult Index()
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            //string contentRootPath = _hostingEnvironment.ContentRootPath;

            //return Content(webRootPath + "\n" + contentRootPath);
			
			return Content("Index LyfrAPI");
        }
    }
}