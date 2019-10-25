using LyfrAPI.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using LyfrAPI.Validations;
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
    public class GeneroController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de email
        //dependencia será injetada nas classes necessarias
        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());

        public GeneroController(LyfrDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Genero generoEnviado)
        {
            try
            {
                if (!ModelState.IsValid || generoEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new GeneroAplicacao(_context, _provedorDiretoriosArquivos).Insert(generoEnviado);
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
                if (!new ValidationFields().ValidateNome(nome))
                {
                    return BadRequest("Gênero inválido! Tente novamente.");
                }
                else
                {
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
                if (!new ValidationFields().ValidateNome(nome))
                {
                    return BadRequest("Nome inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new GeneroAplicacao(_context, _provedorDiretoriosArquivos).DeleteByNome(nome);
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
