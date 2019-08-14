using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APILyfr.Aplicacoes;
using APILyfr.Context;
using APILyfr.Models;
using APILyfr.Security;
using Microsoft.AspNetCore.Http;
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
        public string Insert(string senhaAPI = "",/*[FromBody]*/string json = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
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
        }

        [HttpDelete]
        [Route("DeleteByEmail")]
        public string DeletByEmail(string email, string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
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
        }

        [HttpDelete]
        [Route("DeleteByCPF")]
        public string DeletByCPF(string CPF, string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
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
        }

        [HttpPut]
        [Route("Alter")]
        public string Alter(string json, string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
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
        }


        [HttpGet]
        [Route("GetClienteByEmail")]
        public string GetClienteByEmail(string email, string senha, string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
            {
                try
                {
                    if (email == string.Empty || email == "" || email == null || string.IsNullOrWhiteSpace(email))
                    {
                        return "Email inválido! Tente novamente.";
                    }

                    var resposta = new ClienteAplicacao(_context).GetClienteByEmail(email);

                    if (resposta != null)
                    {
                        if (resposta.Senha != senha)
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
        }

        [HttpGet]
        [Route("GetClienteByCPF")]
        public string GetClienteByCPF(string CPF, string senha, string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
            {
                try
                {
                    if (CPF == string.Empty || CPF == "" || CPF == null || string.IsNullOrWhiteSpace(CPF))
                    {
                        return "CPF inválido! Tente novamente.";
                    }

                    var resposta = new ClienteAplicacao(_context).GetClienteByCPF(CPF);

                    if (resposta != null)
                    {
                        if (resposta.Senha != senha)
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
        }

        [HttpGet]
        [Route("GetAllClientes")]
        public string GetAllCliente(string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
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
}