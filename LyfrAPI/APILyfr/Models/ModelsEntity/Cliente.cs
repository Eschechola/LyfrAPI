using System;
using System.Collections.Generic;

namespace APILyfr.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Favoritos = new HashSet<Favoritos>();
            Historico = new HashSet<Historico>();
            Historicopagamento = new HashSet<Historicopagamento>();
            Livrosclientes = new HashSet<Livrosclientes>();
            Paginasmarcadas = new HashSet<Paginasmarcadas>();
            Sugestao = new HashSet<Sugestao>();
        }

        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string DataNasc { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Plano { get; set; }
        public string Sexo { get; set; }

        public ICollection<Favoritos> Favoritos { get; set; }
        public ICollection<Historico> Historico { get; set; }
        public ICollection<Historicopagamento> Historicopagamento { get; set; }
        public ICollection<Livrosclientes> Livrosclientes { get; set; }
        public ICollection<Paginasmarcadas> Paginasmarcadas { get; set; }
        public ICollection<Sugestao> Sugestao { get; set; }
    }
}
