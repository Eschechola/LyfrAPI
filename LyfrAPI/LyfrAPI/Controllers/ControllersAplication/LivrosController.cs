using LyfrAPI.Aplicacoes.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de email
        //dependencia será injetada nas classes necessarias
        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());

        public LivrosController(LyfrDBContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Livros livroEnviado)
        {
            try
            {
                if (!ModelState.IsValid || livroEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new LivroAplicacao(_context, _provedorDiretoriosArquivos).Insert(livroEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }
    }
}