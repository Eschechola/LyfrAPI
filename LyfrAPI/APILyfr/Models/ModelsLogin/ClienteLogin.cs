using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APILyfr.Models.ModelsLogin
{
    public class ClienteLogin
    {
        [Required]
        [RegularExpression(@"\w\d*")]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public string Cpf { get; set; }
    }
}
