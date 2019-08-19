using System;
using System.Collections.Generic;

namespace APILyfr.Models
{
    public partial class Livrosclientes
    {
        public int IdLivrosClientes { get; set; }
        public int? FkIdCliente { get; set; }
        public int? FkIdLivro { get; set; }
        public int? Nota { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
        public Livros FkIdLivroNavigation { get; set; }
    }
}
