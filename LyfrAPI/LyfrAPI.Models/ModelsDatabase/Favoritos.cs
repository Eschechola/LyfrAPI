using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Favoritos
    {
        public int Id_Favoritos { get; set; }

        [Required(ErrorMessage = "O id do livro deve ser inserido")]
        public int? FkIdLivro { get; set; }

        [Required(ErrorMessage = "O id do cliente deve ser inserido")]
        public int? FkIdCliente { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
        public Livros FkIdLivroNavigation { get; set; }
    }
}
