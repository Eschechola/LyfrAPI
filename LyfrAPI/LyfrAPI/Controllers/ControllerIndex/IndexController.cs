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
            //new EmailMessages().WelcomeEmail("lucas.eschechola@outlook.com");
            //new EmailMessages().WelcomeEmail("lucas.eschechola@gmail.com");
            return "Index";
        }
    }
}