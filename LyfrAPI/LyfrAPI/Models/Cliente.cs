using System;
using System.Collections.Generic;

namespace LyfrAPI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            LivrosClientes = new HashSet<LivrosClientes>();
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
        public char Sexo { get; set; }
        public char Plano { get; set; }

        public ICollection<LivrosClientes> LivrosClientes { get; set; }
    }
}
