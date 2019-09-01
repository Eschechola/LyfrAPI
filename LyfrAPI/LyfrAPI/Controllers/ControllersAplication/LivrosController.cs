using LyfrAPI.Context;
using Microsoft.AspNetCore.Mvc;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        public LivrosController(LyfrDBContext context)
        {
            _context = context;
        }
    }
}