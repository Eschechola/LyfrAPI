using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Sugestao
    {
        public int IdSugestao { get; set; }
        public int? FkIdCliente { get; set; }

        [Required(ErrorMessage ="A mensagem deve ser inserida.")]
        [RegularExpression(@"\w\D*", ErrorMessage = "A mensagem só pode conter letras e números")]
        [MinLength(10, ErrorMessage = "A mensagem deve conter no mínimo 10 caracteres")]
        [MaxLength(550, ErrorMessage = "A mensagem deve conter no máximo 550 caracteres")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O campo atendido deve ser inserido.")]
        [RegularExpression(@"\w\D*", ErrorMessage = "O campo atendido só aceita 'S' ou 'N'")]
        public char Atendido { get; set; }

        public Cliente FkIdClienteNavigation { get; set; }
    }
}
