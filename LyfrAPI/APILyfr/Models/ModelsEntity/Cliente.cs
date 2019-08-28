using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [RegularExpression(@"\w\D*")]
        [MinLength(3)]
        [MaxLength(80)]
        public string Nome { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$")]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [MinLength(5)]
        [MaxLength(70)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Rua { get; set; }

        [Required]
        [MaxLength(30)]
        public string Numero { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}-?\d{3}$")]
        [StringLength(9)]
        public string Cep { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(5)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression(@"\w\D*")]
        public string Estado { get; set; }

        [Required]
        [RegularExpression(@"(\d{2}\/\d{2}\/\d{4})")]
        [StringLength(10)]
        public string DataNasc { get; set; }

        [Required]
        [MinLength(6)]
        public string Senha { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        [StringLength(1)]
        public string Plano { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        public ICollection<Favoritos> Favoritos { get; set; }
        public ICollection<Historico> Historico { get; set; }
        public ICollection<Historicopagamento> Historicopagamento { get; set; }
        public ICollection<Livrosclientes> Livrosclientes { get; set; }
        public ICollection<Paginasmarcadas> Paginasmarcadas { get; set; }
        public ICollection<Sugestao> Sugestao { get; set; }
    }
}
