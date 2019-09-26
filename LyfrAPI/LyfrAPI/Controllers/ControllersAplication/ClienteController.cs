using System;
using LyfrAPI.Context;
using LyfrAPI.Aplicacoes;
using LyfrAPI.Models;
using LyfrAPI.Models.ModelsLogin;
using LyfrAPI.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        private readonly IHostingEnvironment _hostingEnvironment;

        public ClienteController(LyfrDBContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Cliente clienteEnviado)
        {
            try
            {
                if (!ModelState.IsValid || clienteEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    //deixa o email minúsculo para poder inserir no banco
                    clienteEnviado.Email = clienteEnviado.Email.ToLower();

                    var resposta = new ClienteAplicacao(_context, Directory.GetCurrentDirectory()).Insert(clienteEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpDelete]
        [Route("DeleteByEmail")]
        [Authorize]
        public IActionResult DeletByEmail([FromBody]string email)
        {
            try
            {
                if (!new ValidationFields().ValidateEmail(email))
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _hostingEnvironment.WebRootPath).DeleteByEmail(email);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpDelete]
        [Route("DeleteByCPF")]
        [Authorize]
        public IActionResult DeletByCPF([FromBody]string CPF)
        {

            try
            {
                if (!new ValidationFields().ValidateCpf(CPF))
                {
                    return BadRequest("CPF inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _hostingEnvironment.WebRootPath).DeleteByCPF(CPF);
                    return Ok(resposta);
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
        public IActionResult Update([FromBody]Cliente clienteEnviado)
        {
            try
            {
                if (!ModelState.IsValid || clienteEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _hostingEnvironment.WebRootPath).Update(clienteEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }


        [HttpPost]
        [Route("GetClienteByEmail")]
        [Authorize]
        public IActionResult GetClienteByEmail([FromBody]ClienteLogin clienteEnviado)
        {
            try
            {
                if (!new ValidationFields().ValidateEmail(clienteEnviado.Email) || clienteEnviado.Email == null)
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _hostingEnvironment.WebRootPath).GetClienteByEmail(clienteEnviado.Email);

                    if (resposta != null)
                    {
                        if (resposta.Senha != clienteEnviado.Senha)
                        {
                            return BadRequest("Email e/ou senha inválidos");
                        }
                        else
                        {
                            var clienteResposta = JsonConvert.SerializeObject(resposta);
                            return Ok(clienteResposta);
                        }
                    }
                    else
                    {
                        return BadRequest("Cliente não cadastrado!");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPost]
        [Route("GetClienteByCPF")]
        [Authorize]
        public IActionResult GetClienteByCPF([FromBody]ClienteLogin clienteEnviado)
        {
            try
            {
                if (!new ValidationFields().ValidateCpf(clienteEnviado.Cpf) || clienteEnviado.Cpf == null)
                {
                    return BadRequest("CPF inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _hostingEnvironment.WebRootPath).GetClienteByCPF(clienteEnviado.Cpf);

                    if (resposta != null)
                    {
                        if (resposta.Senha != clienteEnviado.Senha)
                        {
                            return BadRequest("CPF e/ou senha inválidos");
                        }
                        else
                        {
                            var clienteResposta = JsonConvert.SerializeObject(resposta);
                            return Ok(clienteResposta);
                        }
                    }
                    else
                    {
                        return BadRequest("Usuário não encontrado!");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpGet]
        [Route("GetAllClientes")]
        [Authorize]
        public IActionResult GetAllClientes(int numeroDeClientes = 0)
        {
            try
            {
                
                var listaDeClientes = new ClienteAplicacao(_context, _hostingEnvironment.WebRootPath).GetAllClientes(numeroDeClientes);

                if (listaDeClientes != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeClientes);
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
        [Route("ForgotPassword")]
        [Authorize]
        public IActionResult ForgotPassword([FromBody]string email)
        {
            try
            {
                if(!new ValidationFields().ValidateEmail(email))
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _hostingEnvironment.WebRootPath).ForgotPassword(email);
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