using System;
using LyfrAPI.Aplicacoes.Aplicacoes;
using LyfrAPI.Context;
using Microsoft.AspNetCore.Mvc;

namespace LyfrAPI.Controllers.ControllersAplication
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeralController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        public GeralController(LyfrDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetInformacoesSite")]
        public IActionResult GetInformacoesSite()
        {
            try
            {
                var informacoes = new GeralAplicacao(_context).GetInformacoesSite();
                return Ok(informacoes);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

    }
}