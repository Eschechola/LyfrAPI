using LyfrAPI.Emails.Functions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public string Index()
        {
            return "Index";
        }
    }
}