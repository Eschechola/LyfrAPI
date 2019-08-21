using System;
using APILyfr.Aplicacoes;
using APILyfr.Context;
using APILyfr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APILyfr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly LyfrDBContext _context = new LyfrDBContext();

        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody]Cliente clienteEnviado)
        {
            try
            {
                if (clienteEnviado==null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context).Insert(clienteEnviado);
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
                if (email == string.Empty || email == "" || email == null || string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context).DeleteByEmail(email);
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
                if (CPF == string.Empty || CPF == "" || CPF == null || string.IsNullOrWhiteSpace(CPF))
                {
                    return BadRequest("CPF inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context).DeleteByCPF(CPF);
                    return Ok(resposta);
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
        public IActionResult Alter([FromBody]Cliente clienteEnviado)
        {
            try
            {
                if (clienteEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context).Alter(clienteEnviado);
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
        public IActionResult GetClienteByEmail([FromBody]Cliente clienteEnviado)
        {
            try
            {
                if (clienteEnviado.Email == null)
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }

                var resposta = new ClienteAplicacao(_context).GetClienteByEmail(clienteEnviado.Email);

                if (resposta != null)
                {
                    if (resposta.Senha != clienteEnviado.Senha)
                    {
                        return BadRequest("Login e/ou senha inválidos");
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
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpPost]
        [Route("GetClienteByCPF")]
        [Authorize]
        public IActionResult GetClienteByCPF([FromBody]Cliente clienteEnviado)
        {
            try
            {
                if (clienteEnviado.Cpf == null)
                {
                    return BadRequest("CPF inválido! Tente novamente.");
                }

                var resposta = new ClienteAplicacao(_context).GetClienteByCPF(clienteEnviado.Cpf);

                if (resposta != null)
                {
                    if (resposta.Senha != clienteEnviado.Senha)
                    {
                        return BadRequest("Login e/ou senha inválidos");
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
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }

        }

        [HttpGet]
        [Route("GetAllClientes")]
        [Authorize]
        public IActionResult GetAllClientes()
        {
            try
            {
                var listaDeClientes = new ClienteAplicacao(_context).GetAllClientes();

                if (listaDeClientes != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeClientes);
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

    }
}