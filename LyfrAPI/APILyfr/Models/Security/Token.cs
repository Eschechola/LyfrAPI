using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILyfr.Models.Security
{
    public class Token
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        // 'M' - Mobile | 'W' - Web | 'A' - Adm
        public char TipoUsuario { get; set; }
    }
}
