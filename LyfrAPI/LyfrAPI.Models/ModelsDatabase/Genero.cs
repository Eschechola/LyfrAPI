using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Genero
    {
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "O nome deve ser inserido.")]
        [RegularExpression(@"\w\D*", ErrorMessage ="O nome deve conter apenas letras.")]
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres.")]
        [MaxLength(40, ErrorMessage = "O nome deve conter no máximo 40 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A foto deve ser inserida.")]
        public string Foto { get; set; }
    }
}
