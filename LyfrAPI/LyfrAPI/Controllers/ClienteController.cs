using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LyfrAPI.Aplicacoes;
using LyfrAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LyfrAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly DBLyfrContext _context = new DBLyfrContext();

        [HttpPost]
        [Route("Insert")]
        public string Insert(/*[FromBody]*/string json = "")
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
        public string DeletByEmail(string email)
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
        public string DeletByCPF(string CPF)
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
        public string Alter(string json)
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


        [HttpGet]
        [Route("GetClienteByEmail")]
        public string GetClienteByEmail(string email)
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
                    var clienteResposta = JsonConvert.SerializeObject(resposta);
                    return clienteResposta;
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
        [Route("GetClienteByCPF")]
        public string GetClienteByCPF(string CPF)
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
                    var clienteResposta = JsonConvert.SerializeObject(resposta);
                    return clienteResposta;
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
        public string GetAllCliente()
        {
            try
            {
                var listaDeClientes = new ClienteAplicacao(_context).GetAllClientes();

                if (listaDeClientes!=null)
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