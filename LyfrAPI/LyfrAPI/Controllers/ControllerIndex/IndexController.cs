using LyfrAPI.Emails.Functions;
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
        public IActionResult Index()
        {
            new ClienteMessages2().WelcomeEmail("lucas.eschechola@gmail.com", "Eschechola");
			return Content("Index LyfrAPI");
        }
    }
}