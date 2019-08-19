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
        public string Insert([FromBody]string json = "")
        {

            try
            {
                if (json == string.Empty || json == "" || json == null || string.IsNullOrWhiteSpace(json))
                {
                    return "Dados inválidos! Tente novamente.";
                }
                else
                {
                    var cliente = JsonConvert.DeserializeObject<Cliente>(json);

                    var resposta = new ClienteAplicacao(_context).Insert(cliente);
                    return resposta;
                }
            }
            catch (Exception)
            {
                return "Erro ao comunicar com a base de dados!";
            }

        }

        [HttpDelete]
        [Route("DeleteByEmail")]
        [Authorize]
        public string DeletByEmail([FromBody]string email)
        {


            try
            {
                if (email == string.Empty || email == "" || email == null || string.IsNullOrWhiteSpace(email))
                {
                    return "Email inválido! Tente novamente.";
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context).DeleteByEmail(email);
                    return resposta;
                }
            }
            catch (Exception)
            {
                return "Erro ao comunicar com a base de dados!";
            }

        }

        [HttpDelete]
        [Route("DeleteByCPF")]
        [Authorize]
        public string DeletByCPF([FromBody]string CPF)
        {

            try
            {
                if (CPF == string.Empty || CPF == "" || CPF == null || string.IsNullOrWhiteSpace(CPF))
                {
                    return "CPF inválido! Tente novamente.";
                }
                else
                {
                    var resposta = new ClienteAplicacao(_context).DeleteByCPF(CPF);
                    return resposta;
                }
            }
            catch (Exception)
            {
                return "Erro ao comunicar com a base de dados!";
            }

        }

        [HttpPut]
        [Route("Alter")]
        [Authorize]
        public string Alter([FromBody]string json)
        {


            var clienteAlterado = new Cliente();
            try
            {
                if (json == string.Empty || json == "" || json == null || string.IsNullOrWhiteSpace(json))
                {
                    return "Dados inválidos! Tente novamente.";
                }
                else
                {
                    clienteAlterado = JsonConvert.DeserializeObject<Cliente>(json);
                    var resposta = new ClienteAplicacao(_context).Alter(clienteAlterado);
                    return resposta;
                }
            }
            catch (Exception)
            {
                return "Erro ao comunicar com a base de dados!";
            }

        }


        [HttpPost]
        [Route("GetClienteByEmail")]
        [Authorize]
        public string GetClienteByEmail([FromBody]string json)
        {
            var cliente = JsonConvert.DeserializeObject<Cliente>(json);

            try
            {
                if (cliente.Email == string.Empty || cliente.Email == "" || cliente.Email == null || string.IsNullOrWhiteSpace(cliente.Email))
                {
                    return "Email inválido! Tente novamente.";
                }

                var resposta = new ClienteAplicacao(_context).GetClienteByEmail(cliente.Email);

                if (resposta != null)
                {
                    if (resposta.Senha != cliente.Senha)
                    {
                        return "Login e/ou senha inválidos";
                    }
                    else
                    {
                        var clienteResposta = JsonConvert.SerializeObject(resposta);
                        return clienteResposta;
                    }
                }
                else
                {
                    return "Cliente não cadastrado!";
                }

            }
            catch (Exception)
            {
                return "Erro ao comunicar com a base de dados!";
            }

        }

        [HttpPost]
        [Route("GetClienteByCPF")]
        [Authorize]
        public string GetClienteByCPF([FromBody]string json)
        {
            var cliente = JsonConvert.DeserializeObject<Cliente>(json);
            try
            {
                if (cliente.Cpf == string.Empty || cliente.Cpf == "" || cliente.Cpf == null || string.IsNullOrWhiteSpace(cliente.Cpf))
                {
                    return "CPF inválido! Tente novamente.";
                }

                var resposta = new ClienteAplicacao(_context).GetClienteByCPF(cliente.Cpf);

                if (resposta != null)
                {
                    if (resposta.Senha != cliente.Senha)
                    {
                        return "Login e/ou senha inválidos";
                    }
                    else
                    {
                        var clienteResposta = JsonConvert.SerializeObject(resposta);
                        return clienteResposta;
                    }
                }
                else
                {
                    return "Cliente não cadastrado!";
                }

            }
            catch (Exception)
            {
                return "Erro ao comunicar com a base de dados!";
            }

        }

        [HttpGet]
        [Route("GetAllClientes")]
        [Authorize]
        public string GetAllCliente()
        {

            try
            {
                var listaDeClientes = new ClienteAplicacao(_context).GetAllClientes();

                if (listaDeClientes != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeClientes);
                    return resposta;
                }
                else
                {
                    return "Erro ao comunicar com a base de dados!";
                }
            }
            catch (Exception)
            {
                return "Erro ao comunicar com a base de dados!";
            }
        }

    }
}