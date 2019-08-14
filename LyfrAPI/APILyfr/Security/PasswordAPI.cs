using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILyfr.Security
{
    public class PasswordAPI
    {
        // Senha para poder fazer requisições a API
        private string Senha;

        public PasswordAPI()
        {
            Senha = "Lyfr123"; 
        }

        public string ReturnPassword()
        {
            return Senha;
        }
    }
}
