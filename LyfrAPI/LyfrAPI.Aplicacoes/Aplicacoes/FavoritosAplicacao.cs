using LyfrAPI.Context;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LyfrAPI.Models.ModelsDatabase;

namespace LyfrAPI.Aplicacoes.Aplicacoes
{
    public class FavoritosAplicacao
    {
        private LyfrDBContext _context;

        public FavoritosAplicacao(LyfrDBContext context)
        {
            _context = context;
        }


        public string Insert(Favoritos favoritos)
        {
            try
            {
                if (favoritos != null)
                {
                    _context.Add(favoritos);
                    _context.SaveChanges();

                    return "Favoritos cadastrado com sucesso!";

                }
                else
                {
                    return "Favoritos é nula! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public List<Livros> GetFavoritosByUsuario(int idUsuario)
        {
            try
            {
                var queryNoBanco = from f in _context.Favoritos
                                   join c in _context.Cliente on f.FkIdCliente equals c.IdCliente
                                   join l in _context.Livros on f.FkIdLivro equals l.IdLivro
                                   where idUsuario.Equals(f.FkIdCliente)
                                   select new FavoritosData
                                   {
                                       IdFavoritos = f.Id_Favoritos,
                                       EmailCliente = c.Email,
                                       TituloLivro = l.Titulo
                                   };

                var favoritosRetornar = queryNoBanco.Select(x => x).ToList();

                var listaDeLivros = new List<Livros>();

                foreach(var item in favoritosRetornar)
                {
                    listaDeLivros.Add(_context.Livros.Where(x => x.Titulo.Equals(item.TituloLivro)).ToList()[0]);
                }

                return listaDeLivros;
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

                    var favoritoPegar = _context.Favoritos.Where(x => x.FkIdLivro.Equals(idLivro) && x.FkIdCliente.Equals(idCliente)).ToList().FirstOrDefault();

                    if (favoritoPegar != null)
                    {
                        _context.Favoritos.Remove(favoritoPegar);
                        _context.SaveChanges();
                        return "O livro de id " + idLivro + " foi deletado com sucesso da sua lista";
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
