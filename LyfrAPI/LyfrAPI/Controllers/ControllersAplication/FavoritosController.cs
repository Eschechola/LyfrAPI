using System;
using LyfrAPI.Aplicacoes.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        public FavoritosController(LyfrDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        [Route("Insert")]
        public IActionResult Insert(Favoritos favoritosEnviado)
        {
            try
            {
                if (!ModelState.IsValid || favoritosEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new FavoritosAplicacao(_context).Insert(favoritosEnviado);
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
        [Route("GetFavoritosByUsuario/{idUsuario}")]
        public IActionResult GetFavoritosByUsuario (int idUsuario = -1)
        {
            try 
            {
                if(idUsuario < 0)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new FavoritosAplicacao(_context).GetFavoritosByUsuario(idUsuario);
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
                    var resposta = new FavoritosAplicacao(_context).Delete(idUsuario, idLivro);
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