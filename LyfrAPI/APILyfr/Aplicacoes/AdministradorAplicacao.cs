using APILyfr.Context;
using APILyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILyfr.Aplicacoes
{
    public class AdministradorAplicacao
    {
        private readonly LyfrDBContext _context;

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
                    if (GetAdmin(admin.Login) != null)
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

        public Administrador GetAdmin(string email)
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
    }
}
