using System;
using System.Collections.Generic;

namespace LyfrAPI.Models
{
    public partial class Historico
    {
        public int IdHistorico { get; set; }
        public int? FkIdLivro { get; set; }
        public int? FkIdCliente { get; set; }
        public string DataLeitura { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
        public Livros FkIdLivroNavigation { get; set; }
    }
}
