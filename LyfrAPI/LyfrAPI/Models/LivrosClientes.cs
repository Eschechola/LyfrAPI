using System;
using System.Collections.Generic;

namespace LyfrAPI.Models
{
    public partial class LivrosClientes
    {
        public int IdConexao { get; set; }
        public int? FkIdCliente { get; set; }
        public int? FkIdLivro { get; set; }
        public int? Nota { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
        public Livros FkIdLivroNavigation { get; set; }
    }
}
