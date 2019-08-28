using System;
using APILyfr.Aplicacoes;
using APILyfr.Context;
using APILyfr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly LyfrDBContext _context = new LyfrDBContext();

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Administrador adminEnviado)
        {
            try
            {
                if (adminEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else if (ModelState.IsValid)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    if (adminEnviado.Cpf == "" || adminEnviado.Email =="" || adminEnviado.Login == ""|| adminEnviado.Senha=="")
                    {
                        return BadRequest("Preencha todos os campos e tente novamente!");
                    }

                    var resposta = new AdministradorAplicacao(_context).Insert(adminEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpPost]
        [Route("GetAdministrador")]
        [Authorize]
        public IActionResult GetAdministrador([FromBody]Administrador adminEnviado)
        {
            try
            {
                if (adminEnviado == null || adminEnviado.Login == "" || adminEnviado.Senha =="")
                {
                    return BadRequest("Login inválido! Tente novamente.");
                }

                var resposta = new AdministradorAplicacao(_context).GetAdminByLogin(adminEnviado.Login);

                if (resposta != null)
                {
                    if (resposta.Senha != adminEnviado.Senha)
                    {
                        return BadRequest("Login ou senha inválidos");
                    }
                    else
                    {
                        var clienteResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(clienteResposta);
                    }
                }
                else
                {
                    return BadRequest("Administrador não cadastrado!");
                }

            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpGet]
        [Route("GetAllAdministradores")]
        [Authorize]
        public IActionResult GetAllAdministradores()
        {
            try
            {
                var listaDeAdministradores = new AdministradorAplicacao(_context).GetAllAdministradores();

                if (listaDeAdministradores != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeAdministradores);
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

        [HttpPut]
        [Route("Alter")]
        [Authorize]
        public IActionResult Alter([FromBody]Administrador adminEnviado)
        {
            try
            {
                if (adminEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new AdministradorAplicacao(_context).Alter(adminEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpDelete]
        [Route("DeleteByLogin")]
        [Authorize]
        public IActionResult DeletByLogin([FromBody]string login)
        {
            try
            {
                if (login == string.Empty || login == "" || login == null || string.IsNullOrWhiteSpace(login))
                {
                    return BadRequest("Login inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new AdministradorAplicacao(_context).DeleteByLogin(login);
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