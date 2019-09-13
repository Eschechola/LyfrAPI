using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public string Index()
        {
            return "Index page - Lyfr API";
        }
    }
}