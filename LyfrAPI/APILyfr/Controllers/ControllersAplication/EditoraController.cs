using APILyfr.Aplicacoes;
using APILyfr.Context;
using APILyfr.Models;
using APILyfr.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditoraController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly LyfrDBContext _context = new LyfrDBContext();

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Editora editoraEnviada)
        {
            try
            {
                if (!ModelState.IsValid|| editoraEnviada == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new EditoraAplicacao(_context).Insert(editoraEnviada);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetEditoraByNome")]
        [Authorize]
        public IActionResult GetEditoraByNome([FromBody]string nome)
        {
            try
            {
                if (new ValidationFields().ValidateNome(nome))
                {
                    return BadRequest("Editora inválida! Tente novamente.");
                }
                else
                {
                    var resposta = new EditoraAplicacao(_context).GetEditoraByNome(nome);

                    if (resposta != null)
                    {
                        if (resposta.Nome == "")
                        {
                            return BadRequest("Nome inválido");
                        }
                        else
                        {
                            var EditoraResposta = JsonConvert.SerializeObject(resposta);
                            return Ok(EditoraResposta);
                        }
                    }
                    else
                    {
                        return BadRequest("Editora não cadastrado!");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpGet]
        [Route("GetAllEditoras")]
        [Authorize]
        public IActionResult GetAllEditoras()
        {
            try
            {
                var listaDeEditoras = new EditoraAplicacao(_context).GetAllEditoras();

                if (listaDeEditoras != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeEditoras);
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
                if (new ValidationFields().ValidateNome(nome))
                {
                    return BadRequest("Nome inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new EditoraAplicacao(_context).DeleteByNome(nome);
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