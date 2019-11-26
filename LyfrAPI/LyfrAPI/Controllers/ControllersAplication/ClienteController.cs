using System;
using LyfrAPI.Context;
using LyfrAPI.Aplicacoes;
using LyfrAPI.Models;
using LyfrAPI.Models.ModelsLogin;
using LyfrAPI.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.FileProviders;
using LyfrAPI.Models.ModelsEmail;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de email
        //dependencia será injetada nas classes necessarias
        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());

        public ClienteController(LyfrDBContext context)
        {
            _context = context;
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

                    var resposta = new ClienteAplicacao(_context, _provedorDiretoriosArquivos).Insert(clienteEnviado);
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
                if (!new ValidationFields().ValidateCpf(CPF))
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
                    var resposta = new ClienteAplicacao(_context).Update(clienteEnviado);
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
                    var resposta = new ClienteAplicacao(_context).GetClienteByEmail(clienteEnviado.Email);

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
                    var resposta = new ClienteAplicacao(_context).GetClienteByCPF(clienteEnviado.Cpf);

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
        [Route("GetAllClientes/{numeroDeClientes}")]
        [Authorize]
        public IActionResult GetAllClientes(int numeroDeClientes = 0)
        {
            try
            {
                var listaDeClientes = new ClienteAplicacao(_context).GetAllClientes(numeroDeClientes);

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
        public IActionResult ForgotPassword([FromBody]RecoveryPassword recuperarSenha)
        {
            try
            {
                if(!new ValidationFields().ValidateEmail(recuperarSenha.Email))
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _provedorDiretoriosArquivos).ForgotPassword(recuperarSenha);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Tivemos alguns problemas de conexão. Tente novamente mais tarde.");
            }
        }

        [HttpPost]
        [Route("GetClienteToUpdateByEmail")]
        [Authorize]
        public IActionResult GetClienteToUpdateByEmail([FromBody]string email)
        {
            try
            {
                if (!new ValidationFields().ValidateEmail(email))
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context, _provedorDiretoriosArquivos).GetClienteByEmail(email);
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