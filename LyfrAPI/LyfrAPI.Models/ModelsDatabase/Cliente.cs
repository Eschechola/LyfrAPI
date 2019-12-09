using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Favoritos = new HashSet<Favoritos>();
            Historico = new HashSet<Historico>();
            Sugestao = new HashSet<Sugestao>();
        }

        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O nome deve ser inserido.")]
        [RegularExpression(@"\w\D*", ErrorMessage = "O nome deve conter somente letras.")]
        [MinLength(3,ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF deve ser inserido.")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "O CPF está inválido.")]
        [MinLength(11, ErrorMessage = "O nome deve ter no mínimo 11 caracteres.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O email deve ser inserido.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "O email está inválido.")]
        [MinLength(10, ErrorMessage = "O email deve ter no mínimo 10 caracteres")]
        [MaxLength(70, ErrorMessage = "O email deve ter no máximo 70 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        [MaxLength(80, ErrorMessage = "A senha deve ter no máximo 80 caracteres.")]
        public string Senha { get; set; }

        public ICollection<Favoritos> Favoritos { get; set; }
        public ICollection<Historico> Historico { get; set; }
        public ICollection<Sugestao> Sugestao { get; set; }
    }
}
