using System;
using System.Collections.Generic;

namespace LyfrAPI.Models
{
    public partial class Favoritos
    {
        public int IdFavoritos { get; set; }
        public int? FkIdLivro { get; set; }
        public int? FkIdCliente { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
        public Livros FkIdLivroNavigation { get; set; }
    }
}
