using LyfrAPI.Emails.Functions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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
            try
            {
               return Content("Index LyfrAPI");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
    }
}