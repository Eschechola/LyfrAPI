using APILyfr.Aplicacoes;
using APILyfr.Context;
using APILyfr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly LyfrDBContext _context = new LyfrDBContext();

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Genero generoEnviado)
        {
            try
            {
                if (generoEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else if (ModelState.IsValid)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new GeneroAplicacao(_context).Insert(generoEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetGeneroByNome")]
        [Authorize]
        public IActionResult GetGeneroByNome([FromBody]string nome)
        {
            try
            {
                if (nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return BadRequest("Gênero inválido! Tente novamente.");
                }

                var resposta = new GeneroAplicacao(_context).GetGeneroByNome(nome);

                if (resposta != null)
                {
                    if (resposta.Nome == "")
                    {
                        return BadRequest("Nome inválido");
                    }
                    else
                    {
                        var generoResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(generoResposta);
                    }
                }
                else
                {
                    return BadRequest("Genero não cadastrado!");
                }

            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpGet]
        [Route("GetAllGeneros")]
        [Authorize]
        public IActionResult GetAllGeneros()
        {
            try
            {
                var listaDeGeneros = new GeneroAplicacao(_context).GetAllGeneros();

                if (listaDeGeneros != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeGeneros);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Erro ao comunicar com a base de dados!");
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
                if (nome == "" || nome == null ||string.IsNullOrWhiteSpace(nome))
                {
                    return BadRequest("Nome inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new GeneroAplicacao(_context).DeleteByNome(nome);
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