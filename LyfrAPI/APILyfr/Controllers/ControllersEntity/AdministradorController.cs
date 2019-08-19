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

        [HttpPost]
        [Route("GetAdministrador")]
        [Authorize]
        public string GetAdministrador([FromBody]string json)
        {
            var adm = JsonConvert.DeserializeObject<Administrador>(json);
            try
            {
                if (adm.Login == string.Empty || adm.Login == "" || adm.Login == null || string.IsNullOrWhiteSpace(adm.Login))
                {
                    return "Login inválido! Tente novamente.";
                }

                var resposta = new AdministradorAplicacao(_context).GetAdminByLogin(adm.Login);

                if (resposta != null)
                {
                    if (resposta.Senha != adm.Senha)
                    {
                        return "Login ou senha inválidos";
                    }
                    else if (resposta.Login != adm.Login)
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

        [HttpGet]
        [Route("GetAllAdministradores")]
        [Authorize]
        public string GetAllAdministradores()
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

        [HttpPut]
        [Route("Alter")]
        [Authorize]
        public string Alter([FromBody]string json)
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

        [HttpDelete]
        [Route("DeleteByLogin")]
        [Authorize]
        public string DeletByLogin([FromBody]string login)
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