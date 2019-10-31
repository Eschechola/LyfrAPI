using LyfrAPI.Context;
using LyfrAPI.Models.ModelsDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace LyfrAPI.Aplicacoes.Aplicacoes
{
    public class GeralAplicacao
    {
        private LyfrDBContext _context;

        public GeralAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        public GeralQuantidade GetInformacoesSite()
        {
            try
            {
                var informacoesSite = new GeralQuantidade
                {
                    QuantidadeLivros = new LivrosAplicacao(_context).GetAllLivros(0).Count,
                    QuantidadeClientes = new ClienteAplicacao(_context).GetAllClientes(0).Count,
                    QuantidadeAutores = new AutorAplicacao(_context).GetAllAutores(0).Count,
                    QuantidadeEditoras = new EditoraAplicacao(_context).GetAllEditoras(0).Count
                };

                return informacoesSite;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
