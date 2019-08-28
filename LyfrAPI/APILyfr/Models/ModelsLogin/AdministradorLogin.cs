using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APILyfr.Models.ModelsLogin
{
    public class AdministradorLogin
    {
        [Required]
        [RegularExpression(@"\w\d*")]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
