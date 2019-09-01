using System;
using System.Collections.Generic;

namespace LyfrAPI.Models
{
    public partial class Historicopagamento
    {
        public int IdHistoricoPagamento { get; set; }
        public int? FkIdCliente { get; set; }
        public string DataPagam { get; set; }
        public string DataVenc { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
    }
}
