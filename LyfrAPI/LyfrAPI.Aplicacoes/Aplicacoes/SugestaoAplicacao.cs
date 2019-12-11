using LyfrAPI.Context;
using LyfrAPI.Emails.Functions.Messages;
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

                    return "Sugestao enviada com sucesso! Logo entraremos em contato.\nEquipe Lyfr agradece seu feedback";
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

                var sugestao = _context.Sugestao.Where(x => x.IdSugestao == idSugestao).ToList();
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

        public List<SugestaoResposta> GetAllSugestoes()
        {
            try
            {
                var queryNoBanco = from s in _context.Sugestao
                                   join c in _context.Cliente on s.FkIdCliente equals c.IdCliente
                                   select new SugestaoResposta
                                   {
                                       Id = s.IdSugestao,
                                       Cpf = c.Cpf,
                                       Email = c.Email,
                                       Atendido = s.Atendido,
                                       Mensagem = s.Mensagem
                                   };

                var listaDeSugestoes = queryNoBanco.Select(x => x).ToList();

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

        public string UpdateState(int idSugestao=0)
        {
            try
            {
                if (idSugestao > 0)
                {
                    var sugestao = GetSugestaoById(idSugestao);

                    if (sugestao != null)
                    {
                        sugestao.Atendido = 'S';
                        _context.Update(sugestao);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return "Sugestão não encontrada, por favor tente novamente.";
                    }

                    return "Sugestao respondida com sucesso!";
                }
                else
                {
                    return "Id é nulo! Por - favor envie um id válido.";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string EnviarResposta(SugestaoResposta sugestao)
        {
            try
            {
                var infoSugestao = GetAllSugestoes().Where(x => x.Id == sugestao.Id).FirstOrDefault();

                if (infoSugestao != null)
                {
                    var resposta = new SugestaoMessages().RespostaSugestao(sugestao);
                    if (resposta)
                    {
                        //atualiza o status da sugestão
                        UpdateState(infoSugestao.Id);
                        return "Resposta enviada com sucesso!";
                    }
                    else
                    {
                        return "Não foi possível enviar a resposta, tente novamente.";
                    }
                }
                else
                {
                    return "Sugestão não encontrada!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
