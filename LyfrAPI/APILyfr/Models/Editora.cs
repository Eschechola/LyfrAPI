using System;
using System.Collections.Generic;

namespace APILyfr.Models
{
    public partial class Editora
    {
        public Editora()
        {
            Livros = new HashSet<Livros>();
        }

        public int IdEditora { get; set; }
        public string Nome { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
