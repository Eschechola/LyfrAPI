using System;
using System.Collections.Generic;

namespace LyfrAPI.Models
{
    public partial class Autores
    {
        public Autores()
        {
            Livros = new HashSet<Livros>();
        }

        public int IdAutor { get; set; }
        public string Nome { get; set; }
        public string AnoNasc { get; set; }
        public string Bio { get; set; }
        public string Foto { get; set; }

        public ICollection<Livros> Livros { get; set; }
    }
}
