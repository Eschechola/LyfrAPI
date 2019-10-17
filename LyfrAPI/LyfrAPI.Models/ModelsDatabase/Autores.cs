using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
{
    public partial class Autores
    {
        public Autores()
        {
            Livros = new HashSet<Livros>();
        }

        public int IdAutor { get; set; }

        [Required(ErrorMessage = "O nome deve ser inserido.")]
        [RegularExpression(@"\w\D*", ErrorMessage = "O nome deve conter somente letras.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento deve ser inserida.")]
        [RegularExpression(@"(\d{2}\/\d{2}\/\d{4})", ErrorMessage = "A data de nascimento está inválida. Use o modelo DD/MM/YYYY.")]
        public string AnoNasc { get; set; }

        [Required(ErrorMessage = "A mensagem deve ser inserida.")]
        [RegularExpression(@"\w\D*", ErrorMessage = "A mensagem só pode conter letras e números")]
        [MinLength(100, ErrorMessage = "A biografia deve conter no mínimo 100 caracteres")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "A foto deve ser inserida")]
        public string Foto { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
