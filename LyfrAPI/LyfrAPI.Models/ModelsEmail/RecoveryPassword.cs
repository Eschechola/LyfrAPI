using System;
using System.Collections.Generic;
using System.Text;

namespace LyfrAPI.Models.ModelsEmail
{
    public class RecoveryPassword
    {
        public string Email { get; set; }

        public string CodigoGerado { get; set; }
    }
}
