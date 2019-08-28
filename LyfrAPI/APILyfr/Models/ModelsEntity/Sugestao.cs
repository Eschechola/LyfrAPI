using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APILyfr.Models
{
    public partial class Sugestao
    {
        public int idSugestao { get; set; }
        public int? FkIdCliente { get; set; }

        [Required]
        [RegularExpression(@"\w\D*")]
        [MinLength(10)]
        [MaxLength(600)]
        public string Mensagem { get; set; }

        [Required]
        [RegularExpression(@"\w\D*")]
        [StringLength(1)]
        public char Atendido { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
    }
}
