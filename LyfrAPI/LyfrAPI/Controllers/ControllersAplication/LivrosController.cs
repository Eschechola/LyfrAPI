using LyfrAPI.Aplicacoes.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
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
            catch (Exception ex)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("GetAllLivros")]
        [Authorize]
        public IActionResult GetAllLivros(int numeroDeLivros = 0)
        {
            try
            {

                var listaDeLivros = new LivroAplicacao(_context).GetAllLivros(numeroDeLivros);

                if (listaDeLivros != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeLivros);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Nenhum cliente cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }


        [HttpPost]
        [Route("GetLivroByTitulo")]
        [Authorize]
        public IActionResult GetLivroByTitulo([FromBody]string titulo)
        {
            try
            {
                var resposta = new LivroAplicacao(_context).GetLivroByTitulo(titulo);

                if (resposta != null)
                {
                    var livroResposta = JsonConvert.SerializeObject(resposta);
                    return Ok(livroResposta);
                }
                else
                {
                    return BadRequest("Usuário não encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }
    }
}