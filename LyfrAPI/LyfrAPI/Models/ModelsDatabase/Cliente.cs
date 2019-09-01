using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyfrAPI.Models
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

        [Required(ErrorMessage = "O nome deve ser inserido.")]
        [RegularExpression(@"\w\D*", ErrorMessage = "O nome deve conter somente letras.")]
        [MinLength(3,ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF deve ser inserido.")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "O CPF está inválido.")]
        [MinLength(11, ErrorMessage = "O nome deve ter no mínimo 11 caracteres.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O email deve ser inserido.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "O email está inválido.")]
        [MinLength(10, ErrorMessage = "O email deve ter no mínimo 10 caracteres")]
        [MaxLength(70, ErrorMessage = "O email deve ter no máximo 70 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A rua deve ser inserida.")]
        [MaxLength(100, ErrorMessage = "A rua deve ter no máximo 100 caracteres")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O número deve ser inserido.")]
        [MaxLength(10, ErrorMessage = "O numero deve ter no máximo 10 caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O CEP deve ser inserido.")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "O CEP está inválido.")]
        [MinLength(8, ErrorMessage = "O CEP deve ter no mínimo 8 caracteres")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "A cidade deve ser inserida.")]
        [MaxLength(40, ErrorMessage = "A cidade deve ter no máximo 40 caracteres")]
        [MinLength(5, ErrorMessage = "A cidade deve ter no mínimo 5 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado deve ser inserido.")]
        [StringLength(2, ErrorMessage = "O estado só contem 2 caracteres")]
        [RegularExpression(@"\w\D*", ErrorMessage = "O estado só pode conter letras")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "A data de nascimento deve ser inserida.")]
        [RegularExpression(@"(\d{2}\/\d{2}\/\d{4})", ErrorMessage = "A data de nascimento está inválida. Use o modelo DD/MM/YYYY.")]
        public string DataNasc { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        [MaxLength(80, ErrorMessage = "A senha deve ter no máximo 80 caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O telefone deve ser inserido.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O plano deve ser inserido.")]
        [StringLength(1, ErrorMessage = "O plano só pode ser 'P' ou 'G'")]
        public string Plano { get; set; }

        [Required(ErrorMessage = "O sexo deve ser inserido.")]
        [StringLength(1, ErrorMessage = "O sexo só pode ser 'M', 'F' ou 'O'.")]
        public string Sexo { get; set; }

        public ICollection<Favoritos> Favoritos { get; set; }
        public ICollection<Historico> Historico { get; set; }
        public ICollection<Historicopagamento> Historicopagamento { get; set; }
        public ICollection<Livrosclientes> Livrosclientes { get; set; }
        public ICollection<Paginasmarcadas> Paginasmarcadas { get; set; }
        public ICollection<Sugestao> Sugestao { get; set; }
    }
}
