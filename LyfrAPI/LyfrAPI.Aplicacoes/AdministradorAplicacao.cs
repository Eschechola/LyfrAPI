using LyfrAPI.Context;
using LyfrAPI.Emails.Functions;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LyfrAPI.Aplicacoes
{
    public class AdministradorAplicacao
    {
        private LyfrDBContext _context;

        public AdministradorAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        public string Insert(Administrador admin)
        {
            try
            {
                if (admin != null)
                {
                    if (GetAdminByLogin(admin.Login) != null && GetAdminByCPF(admin.Cpf) != null)
                    {
                        return "Administrador já cadastrado na base de dados!";
                    }
                    else
                    {
                        _context.Add(admin);
                        _context.SaveChanges();

                        return "Administrador cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Administrador é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Administrador GetAdminByLogin(string login)
        {
            Administrador primeiroAdmin = new Administrador();

            try
            {
                if (login == string.Empty || login == null || login == "" || string.IsNullOrWhiteSpace(login))
                {
                    return null;
                }

                var administrador = _context.Administrador.Where(x => x.Login == login).ToList();
                primeiroAdmin = administrador.FirstOrDefault();


                if (primeiroAdmin != null)
                {
                    return primeiroAdmin;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Administrador GetAdminByEmail(string email)
        {
            Administrador primeiroAdmin = new Administrador();

            try
            {
                if (email == string.Empty || email == null || email == "" || string.IsNullOrWhiteSpace(email))
                {
                    return null;
                }

                var administrador = _context.Administrador.Where(x => x.Email == email).ToList();
                primeiroAdmin = administrador.FirstOrDefault();


                if (primeiroAdmin != null)
                {
                    return primeiroAdmin;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Administrador GetAdminByCPF(string cpf)
        {
            Administrador primeiroAdmin = new Administrador();

            try
            {
                if (cpf == string.Empty || cpf == null || cpf == "" || string.IsNullOrWhiteSpace(cpf))
                {
                    return null;
                }

                var administrador = _context.Administrador.Where(x => x.Cpf == cpf).ToList();
                primeiroAdmin = administrador.FirstOrDefault();


                if (primeiroAdmin != null)
                {
                    return primeiroAdmin;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Administrador> GetAllAdministradores()
        {
            List<Administrador> listaDeAdministradores = new List<Administrador>();
            try
            {
                listaDeAdministradores = _context.Administrador.Select(x => x).ToList();

                if (listaDeAdministradores != null)
                {
                    return listaDeAdministradores;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Update(Administrador administrador)
        {
            try
            {
                if (administrador == null)
                {
                    return "Dados inválidos! Por favor tente novamente.";
                }
                else
                {
                    if (administrador != null)
                    {
                        _context.Administrador.Update(administrador);
                        _context.SaveChanges();

                        return "Administrador " + administrador.Login + " alterado com sucesso!";
                    }
                    else
                    {
                        return "Administrador não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Já existe um usuário cadastrado com seu Login e/ou CPF.";
            }
        }

        public string DeleteByLogin(string login)
        {
            try
            {
                if (login == string.Empty || login == null || login == "" || string.IsNullOrWhiteSpace(login))
                {
                    return "Login inválido! Por favor tente novamente.";
                }
                else
                {
                    var admin = GetAdminByLogin(login);

                    if (admin != null)
                    {
                        _context.Administrador.Remove(admin);
                        _context.SaveChanges();

                        return "Administrador " + admin.Login + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Administrador não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string ForgotPassword(string emailAdministrador)
        {
            try
            {
                var administrador = GetAdminByEmail(emailAdministrador.ToLower());

                if (administrador != null)
                {
                    var resposta = new EmailMessages().UpdatePasswordEmail(administrador.Email, administrador.Senha);
                    return resposta;
                }
                else
                {
                    return "Administrador não encontrado!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
