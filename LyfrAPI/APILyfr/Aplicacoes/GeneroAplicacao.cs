using APILyfr.Context;
using APILyfr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILyfr.Aplicacoes
{
    public class GeneroAplicacao
    {
        private readonly LyfrDBContext _context;

        public GeneroAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        public string Insert(Genero genero)
        {
            try
            {
                if (genero != null)
                {
                    if (GetGeneroByNome(genero.Nome) != null)
                    {
                        return "Genero já cadastrado na base de dados!";
                    }
                    else
                    {
                        _context.Add(genero);
                        _context.SaveChanges();

                        return "Genero cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Genero é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Genero GetGeneroByNome(string nome)
        {
            Genero primeiroGenero = new Genero();

            try
            {
                if (nome == string.Empty || nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return null;
                }

                var genero = _context.Genero.Where(x => x.Nome == nome).ToList();
                primeiroGenero = genero.FirstOrDefault();


                if (primeiroGenero != null)
                {
                    return primeiroGenero;
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

        public List<Genero> GetAllGeneros()
        {
            List<Genero> listaDeGeneros = new List<Genero>();
            try
            {
                listaDeGeneros = _context.Genero.Select(x => x).ToList();

                if (listaDeGeneros != null)
                {
                    return listaDeGeneros;
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

        public string DeleteByNome(string nome)
        {
            try
            {
                if (nome == string.Empty || nome == null || nome == "" || string.IsNullOrWhiteSpace(nome))
                {
                    return "Genero inválido! Por favor tente novamente.";
                }
                else
                {
                    var genero = GetGeneroByNome(nome);

                    if (genero != null)
                    {
                        _context.Genero.Remove(genero);
                        _context.SaveChanges();

                        return "Genero " + genero.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Genero não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
