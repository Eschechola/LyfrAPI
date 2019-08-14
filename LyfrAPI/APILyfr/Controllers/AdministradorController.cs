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
        public string Insert(string senhaAPI = "", /*[FromBody]*/string json = "")
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
                        var admin = JsonConvert.DeserializeObject<Administrador>(json);

                        if (admin.Login == "" || string.IsNullOrWhiteSpace(admin.Login) ||
                            admin.Senha == "" || string.IsNullOrWhiteSpace(admin.Senha) ||
                            admin.Email == "" || string.IsNullOrWhiteSpace(admin.Cpf) ||
                            admin.Senha == "" || string.IsNullOrWhiteSpace(admin.Cpf))
                        {
                            return "Preencha todos os campos e tente novamente!";
                        }

                        var resposta = new AdministradorAplicacao(_context).Insert(admin);
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
        [Route("GetAdministrador")]
        public string GetAdministrador(string login, string senha, string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
            {
                try
                {
                    if (login == string.Empty || login == "" || login == null || string.IsNullOrWhiteSpace(login) ||
                        senha == string.Empty || senha == "" || senha == null || string.IsNullOrWhiteSpace(senha))
                    {
                        return "Login inválido! Tente novamente.";
                    }

                    var resposta = new AdministradorAplicacao(_context).GetAdminByLogin(login);

                    if (resposta != null)
                    {
                        if (resposta.Senha != senha)
                        {
                            return "Login ou senha inválidos";
                        }
                        else
                        {
                            var clienteResposta = JsonConvert.SerializeObject(resposta);
                            return clienteResposta;
                        }
                    }
                    else
                    {
                        return "Administrador não cadastrado!";
                    }

                }
                catch (Exception)
                {
                    return "Erro ao comunicar com a base de dados!";
                }
            }
        }

        [HttpGet]
        [Route("GetAllAdministradores")]
        public string GetAllAdministradores(string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
            {
                try
                {
                    var listaDeAdministradores = new AdministradorAplicacao(_context).GetAllAdministradores();

                    if (listaDeAdministradores != null)
                    {
                        var resposta = JsonConvert.SerializeObject(listaDeAdministradores);
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
                var adminAlterado = new Administrador();
                try
                {
                    if (json == string.Empty || json == "" || json == null || string.IsNullOrWhiteSpace(json))
                    {
                        return "Dados inválidos! Tente novamente.";
                    }
                    else
                    {
                        adminAlterado = JsonConvert.DeserializeObject<Administrador>(json);
                        var resposta = new AdministradorAplicacao(_context).Alter(adminAlterado);
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
        [Route("DeleteByLogin")]
        public string DeletByLogin(string login, string senhaAPI = "")
        {
            if (senhaAPI != new PasswordAPI().ReturnPassword())
            {
                return "Você não tem acesso a esse recurso!";
            }
            else
            {
                try
                {
                    if (login == string.Empty || login == "" || login == null || string.IsNullOrWhiteSpace(login))
                    {
                        return "Login inválido! Tente novamente.";
                    }
                    else
                    {
                        var resposta = new AdministradorAplicacao(_context).DeleteByLogin(login);
                        return resposta;
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