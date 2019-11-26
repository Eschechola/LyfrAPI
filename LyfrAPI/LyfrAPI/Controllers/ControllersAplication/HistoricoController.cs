using LyfrAPI.Aplicacoes.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        public HistoricoController(LyfrDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        [Route("Insert")]
        public IActionResult Insert(Historico historicoEnviado)
        {
            try
            {
                if (!ModelState.IsValid || historicoEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new HistoricoAplicacao(_context).Insert(historicoEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");

            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetHistoricoByUsuario/{idUsuario}")]
        public IActionResult GetHistoricoByUsuario(int idUsuario = -1)
        {
            try
            {
                if (idUsuario < 0)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new HistoricoAplicacao(_context).GetHistoricoByUsuario(idUsuario);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }


        [HttpDelete]
        [Authorize]
        [Route("DeleteById/{idUsuario}/{idLivro}")]
        public IActionResult DeleteById(int idUsuario = -1, int idLivro = -1)
        {
            try
            {
                if (idUsuario < 0 || idLivro < 0)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new HistoricoAplicacao(_context).Delete(idUsuario, idLivro);
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