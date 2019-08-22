using System;
using System.Collections.Generic;

namespace APILyfr.Models
{
    public partial class Sugestao
    {
        public int idSugestao { get; set; }
        public int? FkIdCliente { get; set; }
        public string Mensagem { get; set; }
        public char Atendido { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
    }
}
