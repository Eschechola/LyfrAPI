using LyfrAPI.Context;
using LyfrAPI.Models;
using LyfrAPI.Models.ModelsDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyfrAPI.Aplicacoes.Aplicacoes
{
    public class HistoricoAplicacao
    {
        private LyfrDBContext _context;

        public HistoricoAplicacao(LyfrDBContext context)
        {
            _context = context;
        }


        public string Insert(Historico historico)
        {
            try
            {
                if (historico != null)
                {
                    _context.Add(historico);
                    _context.SaveChanges();

                    return "Histórico cadastrado com sucesso!";

                }
                else
                {
                    return "Histórico é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public List<HistoricoData> GetHistoricoByUsuario(int idUsuario)
        {
            try
            {
                //realiza uma query no banco de favoritos onde o fkidcliente seja igual ao id do usuario
                //retornando uma lista de favoritos
                var queryNoBanco = from h in _context.Historico
                                   join c in _context.Cliente on h.FkIdCliente equals c.IdCliente
                                   join l in _context.Livros on h.FkIdLivro equals l.IdLivro
                                   where idUsuario.Equals(h.FkIdCliente)
                                   select new HistoricoData
                                   {
                                        NomeLivro = l.Titulo,
                                        DataLeitura = h.DataLeitura
                                   };

                var listaHistorico = queryNoBanco.ToList();

                return listaHistorico;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Delete(int idCliente, int idLivro)
        {
            try
            {
                if (idLivro < 0 || idCliente < 0)
                {

                    return "Insira um ID válido";

                }
                else
                {
                    //pega o historico que tem o id do livro e o id do cliente e deleta do banco
                    var historicoPegar = _context.Historico.Where(x => x.FkIdLivro.Equals(idLivro) && x.FkIdCliente.Equals(idCliente)).ToList().FirstOrDefault();

                    if (historicoPegar != null)
                    {
                        _context.Historico.Remove(historicoPegar);
                        _context.SaveChanges();

                        //pega o livro para informar o nome
                        var livro = new LivrosAplicacao(_context).GetById(idLivro);
                        return "O livro " + livro.Titulo + " foi deletado com sucesso do seu histórico";
                    }
                    else
                    {
                        return "Nenhum livro encontrado na sua lista!";
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
