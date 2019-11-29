using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Livros
    {
        public Livros()
        {
            Favoritos = new HashSet<Favoritos>();
            Historico = new HashSet<Historico>();
        }

        public int IdLivro { get; set; }

        [Required(ErrorMessage = "O Título deve ser inserido.")]
        public string Titulo { get; set; }
        public int FkAutor { get; set; }
        public int FkEditora { get; set; }

        [Required(ErrorMessage = "O ano de lançamento deve ser inserido")]
        [MinLength(4, ErrorMessage = "O ano está inválido")]
        public string Ano_Lanc { get; set; }

        [Required(ErrorMessage = "O Gênero deve ser inserido")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A sinopse deve ser inserida")]
        public string Sinopse { get; set; }

        public string Capa { get; set; }

        public string Arquivo { get; set; }

        [Required(ErrorMessage = "O ISBN deve ser inserido")]
        public string Isbn { get; set; }

        public string Idioma { get; set; }

        public int? TotalAcessos { get; set; }

        public Autores FkAutorNavigation { get; set; }
        public Editora FkEditoraNavigation { get; set; }
        public ICollection<Favoritos> Favoritos { get; set; }
        public ICollection<Historico> Historico { get; set; }
    }
}
