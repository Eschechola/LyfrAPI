using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APILyfr.Models
{
    public partial class Editora
    {
        public Editora()
        {
            Livros = new HashSet<Livros>();
        }

        public int IdEditora { get; set; }

        [Required]
        [RegularExpression(@"\w\D*")]
        [MinLength(3)]
        [MaxLength(70)]
        public string Nome { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
