using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Editora
    {
        public Editora()
        {
            Livros = new HashSet<Livros>();
        }

        public int IdEditora { get; set; }

        [Required(ErrorMessage = "O nome deve ser inserido.")]
        [RegularExpression(@"\w\D*", ErrorMessage = "O nome deve conter apenas letras.")]
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres.")]
        [MaxLength(70, ErrorMessage = "O nome deve conter no máximo 70 caracteres.")]
        public string Nome { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
