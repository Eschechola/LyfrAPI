using System;
using System.Collections.Generic;

namespace APILyfr.Models
{
    public partial class Administrador
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
    }
}
