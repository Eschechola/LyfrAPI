using System;
using System.Collections.Generic;

namespace APILyfr.Models
{
    public partial class Paginasmarcadas
    {
        public int IdPaginasMarcadas { get; set; }
        public int? FkIdLivro { get; set; }
        public int? FkIdCliente { get; set; }
        public int? NumPag { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
        public Livros FkIdLivroNavigation { get; set; }
    }
}
