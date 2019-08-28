using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APILyfr.Models
{
    public partial class Genero
    {
        public int IdGenero { get; set; }

        [Required]
        [RegularExpression(@"\w\D*")]
        [MinLength(3)]
        [MaxLength(40)]
        public string Nome { get; set; }
    }
}
