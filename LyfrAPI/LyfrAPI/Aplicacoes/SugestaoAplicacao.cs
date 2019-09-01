using LyfrAPI.Context;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyfrAPI.Aplicacoes
{
    public class SugestaoAplicacao
    {
        private LyfrDBContext _context;

        public SugestaoAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        public string Insert(Sugestao sugestao)
        {
            try
            {
                if (sugestao != null)
                {
                    _context.Add(sugestao);
                    _context.SaveChanges();

                    return "Sugestao cadastrada com sucesso!";
                }
                else
                {
                    return "Sugestao é nula! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public List<Sugestao> GetSugestoesByIdCliente(int idCliente)
        {
            List<Sugestao> listaDeSugestoes = new List<Sugestao>();

            try
            {
                if (idCliente < 0)
                {
                    return null;
                }

                listaDeSugestoes = _context.Sugestao.Where(x => x.FkIdCliente == idCliente).ToList();
                
                if (listaDeSugestoes != null)
                {
                    return listaDeSugestoes;
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

        public Sugestao GetSugestaoById(int idSugestao)
        {
            Sugestao primeiraSugestao = new Sugestao();

            try
            {
                if (idSugestao < 0)
                {
                    return null;
                }

                var sugestao = _context.Sugestao.Where(x => x.idSugestao == idSugestao).ToList();
                primeiraSugestao = sugestao.FirstOrDefault();


                if (primeiraSugestao != null)
                {
                    return primeiraSugestao;
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

        public List<Sugestao> GetAllSugestoes()
        {
            List<Sugestao> listaDeSugestoes = new List<Sugestao>();

            try
            {
                listaDeSugestoes = _context.Sugestao.Select(x => x).ToList();

                if (listaDeSugestoes != null)
                {
                    return listaDeSugestoes;
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
