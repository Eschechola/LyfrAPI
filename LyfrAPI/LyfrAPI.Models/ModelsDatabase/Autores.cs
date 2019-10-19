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

        [Required(ErrorMessage = "O nome do autor deve ser inserido")]
        [MaxLength(40, ErrorMessage = "O nome do autor deve ter no máximo 40 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O ano de nascimento deve ser inserido")]
        [RegularExpression(@"(\d{2}\/\d{2}\/\d{4})", ErrorMessage = "A data de nascimento está inválida. Use o modelo DD/MM/YYYY.")]
        public string AnoNasc { get; set; }

        [Required(ErrorMessage = "A biografia deve ser inserida")]
        [MinLength(100, ErrorMessage = "A biografia deve ter no mínimo 100 caracteres")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "A foto deve ser inserida")]
        public string Foto { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
