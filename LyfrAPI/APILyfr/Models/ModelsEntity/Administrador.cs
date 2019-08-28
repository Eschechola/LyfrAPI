using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APILyfr.Models
{
    public partial class Administrador
    {
        [Required]
        [RegularExpression(@"\w\d*")]
        [MinLength(3)]
        [MaxLength(40)]
        public string Login { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Senha { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }

        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$")]
        [StringLength(20)]
        public string Cpf { get; set; }
    }
}
