using LyfrAPI.Context;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using LyfrAPI.Aplicacoes;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        public AutorController(LyfrDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Autores autorEnviado)
        {
            try
            {
                if (!ModelState.IsValid || autorEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new AutorAplicacao(_context).Insert(autorEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetAutorByNome")]
        [Authorize]
        public IActionResult GetAutorByNome([FromBody]string nome)
        {
            try
            {

                var resposta = new AutorAplicacao(_context).GetAutorByNome(nome);

                if (resposta != null)
                {
                    var autorResposta = JsonConvert.SerializeObject(resposta);
                    return Ok(autorResposta);
                }
                else
                {
                    return BadRequest("Autor não cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("GetAllAutores")]
        [Authorize]
        public IActionResult GetAllAutores(int numeroDeAutores = 0)
        {
            try
            {
                var listaDeAutores = new AutorAplicacao(_context).GetAllAutores(numeroDeAutores);

                if (listaDeAutores != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeAutores);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Nenhum autor cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpDelete]
        [Route("DeleteByNome")]
        [Authorize]
        public IActionResult DeleteByNome([FromBody]string nome)
        {
            try
            {
                var resposta = new AutorAplicacao(_context).DeleteByNome(nome);
                return Ok(resposta);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }
    }
}