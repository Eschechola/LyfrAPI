using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LyfrAPI.Models.ModelsLogin
{
    public class ClienteLogin
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida")]
        public string Senha { get; set; }

        public string Cpf { get; set; }
    }
}
