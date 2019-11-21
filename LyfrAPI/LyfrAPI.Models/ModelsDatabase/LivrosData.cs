using System;
using System.Collections.Generic;
using System.Text;

namespace LyfrAPI.Models.ModelsDatabase
{
    public class LivrosData
    {
        public int IdLivro { get; set; }
        public string Titulo { get; set; }
        public Autores Autor { get; set; }
        public Editora Editora { get; set; }
        public string Ano_Lanc { get; set; }
        public string Genero { get; set; }
        public string Sinopse { get; set; }
        public string Capa { get; set; }
        public string Arquivo { get; set; }
        public string Isbn { get; set; }
        public string Idioma { get; set; }
        public float? IdMediaNota { get; set; }
        public int? TotalAcessos { get; set; }
    }
}
