using LyfrAPI.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SugestaoController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        public SugestaoController(LyfrDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Sugestao sugestaoEnviada)
        {
            //try
            //{
                if (!ModelState.IsValid|| sugestaoEnviada == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {

                    var resposta = new SugestaoAplicacao(_context).Insert(sugestaoEnviada);
                    return Ok(resposta);
                }
            //}
            //catch (Exception)
            //{
            //    return BadRequest("Erro ao comunicar com a base de dados!");
            //}
        }

        [HttpPost]
        [Route("GetSugestoesByIdCliente")]
        [Authorize]
        public IActionResult GetSugestoesByIdCliente([FromBody]int idCliente)
        {
            try
            {
                if (idCliente < 0)
                {
                    return BadRequest("Id inválido! Tente novamente.");
                }

                var resposta = new SugestaoAplicacao(_context).GetSugestoesByIdCliente(idCliente); 

                if (resposta != null)
                {
                    var listaDeSugestoes = JsonConvert.SerializeObject(resposta);
                    return Ok(listaDeSugestoes);
                }
                else
                {
                    return BadRequest("Esse usuário não tem nenhuma sugestão!");
                }

            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetSugestoesById")]
        [Authorize]
        public IActionResult GetSugestoesById([FromBody]int idSugestao)
        {
            try
            {
                if (idSugestao < 0)
                {
                    return BadRequest("Id inválido! Tente novamente.");
                }

                var resposta = new SugestaoAplicacao(_context).GetSugestaoById(idSugestao);

                if (resposta != null)
                {
                    var sugestao = JsonConvert.SerializeObject(resposta);
                    return Ok(sugestao);
                }
                else
                {
                    return BadRequest("Sugestão não encontrada!");
                }

            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpGet]
        [Route("GetAllSugestoes")]
        [Authorize]
        public IActionResult GetAllSugestoes()
        {
            try
            {
                var listaDeSugestoes = new SugestaoAplicacao(_context).GetAllSugestoes();

                if (listaDeSugestoes != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeSugestoes);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Nenhum registro encontrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }
    }
}