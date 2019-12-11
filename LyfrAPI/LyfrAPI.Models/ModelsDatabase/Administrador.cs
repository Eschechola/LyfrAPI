using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Administrador
    {
        [Required(ErrorMessage = "O login deve ser inserido.")]
        [RegularExpression(@"\w*", ErrorMessage = "O login deve conter apenas letras e números")]
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres")]
        [MaxLength(40, ErrorMessage = "O nome deve conter no máximo 40 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida.")]
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome deve conter no máximo 50 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O email deve ser inserido.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "O CPF está inválido.")]
        [MinLength(5, ErrorMessage = "O nome deve conter no mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome deve conter no máximo 50 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF deve ser inserido.")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$")]
        [MinLength(11, ErrorMessage = "O nome deve conter no mínimo 11 caracteres)")]
        public string Cpf { get; set; }

        public int IdAdministrador { get; set; }
    }
}
