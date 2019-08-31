using System;
using System.Collections.Generic;

namespace APILyfr.Models
{
    public partial class Livros
    {
        public Livros()
        {
            Favoritos = new HashSet<Favoritos>();
            Historico = new HashSet<Historico>();
            Livrosclientes = new HashSet<Livrosclientes>();
            Paginasmarcadas = new HashSet<Paginasmarcadas>();
        }

        public int IdLivro { get; set; }
        public string Titulo { get; set; }
        public int? FkAutor { get; set; }
        public int? FkEditora { get; set; }
        public string AnoNasc { get; set; }
        public string Genero { get; set; }
        public string Sinopse { get; set; }
        public string Capa { get; set; }
        public string Arquivo { get; set; }
        public string Isbn { get; set; }
        public string Idioma { get; set; }
        public float? IdMediaNota { get; set; }
        public int? TotalAcessos { get; set; }

        public Autores FkAutorNavigation { get; set; }
        public Editora FkEditoraNavigation { get; set; }
        public ICollection<Favoritos> Favoritos { get; set; }
        public ICollection<Historico> Historico { get; set; }
        public ICollection<Livrosclientes> Livrosclientes { get; set; }
        public ICollection<Paginasmarcadas> Paginasmarcadas { get; set; }
    }
}
