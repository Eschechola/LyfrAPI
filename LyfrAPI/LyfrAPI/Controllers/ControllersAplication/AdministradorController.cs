using System;
using System.IO;
using LyfrAPI.Aplicacoes;
using LyfrAPI.Context;
using LyfrAPI.Models;
using LyfrAPI.Models.ModelsEmail;
using LyfrAPI.Models.ModelsLogin;
using LyfrAPI.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de email
        //dependencia será injetada nas classes necessarias
        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());

        public AdministradorController(LyfrDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Administrador adminEnviado)
        {
            try
            {
                if (!ModelState.IsValid || adminEnviado == null)
                {
                    return BadRequest("Preencha todos os campos CORRETAMENTE e tente novamente!");
                }
                else
                {
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
        [Route("GetAdministradorByEmail")]
        [Authorize]
        public IActionResult GetAdministradorByEmail([FromBody]string email)
        {
            try
            {
                if (email.Equals(string.Empty))
                {
                    return BadRequest("Login inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new AdministradorAplicacao(_context).GetAdminByEmail(email);

                    if (resposta != null)
                    {
                        var adminResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(adminResposta);
                    }
                    else
                    {
                        return BadRequest("Administrador não cadastrado!");
                    }
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
        public IActionResult GetAdministrador([FromBody]AdministradorLogin adminEnviado)
        {
            try
            {
                if (!ModelState.IsValid || adminEnviado == null)
                {
                    return BadRequest("Login inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new AdministradorAplicacao(_context).GetAdminByLogin(adminEnviado.Login);

                    if (resposta != null)
                    {
                        if (resposta.Senha != adminEnviado.Senha)
                        {
                            return BadRequest("Login ou senha inválidos");
                        }
                        else
                        {
                            var adminResposta = JsonConvert.SerializeObject(resposta);
                            return Ok(adminResposta);
                        }
                    }
                    else
                    {
                        return BadRequest("Administrador não cadastrado!");
                    }
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
        [Route("Update")]
        [Authorize]
        public IActionResult Update([FromBody]Administrador adminEnviado)
        {
            try
            {
                if (!ModelState.IsValid || adminEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new AdministradorAplicacao(_context).Update(adminEnviado);
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
                if (!new ValidationFields().ValidateLogin(login))
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

        [HttpPost]
        [Route("ForgotPassword")]
        [Authorize]
        public IActionResult ForgotPassword([FromBody]RecoveryPassword recuperarSenha)
        {
            try
            {
                if (!new ValidationFields().ValidateEmail(recuperarSenha.Email))
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new AdministradorAplicacao(_context, _provedorDiretoriosArquivos).ForgotPassword(recuperarSenha);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Tivemos alguns problemas de conexão. Tente novamente mais tarde.");
            }
        }
    }
}