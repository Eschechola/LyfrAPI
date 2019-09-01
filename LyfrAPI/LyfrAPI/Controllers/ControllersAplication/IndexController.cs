using Microsoft.AspNetCore.Mvc;

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