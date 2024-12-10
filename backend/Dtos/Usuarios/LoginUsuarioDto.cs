using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.Usuarios
{
    public class LoginUsuarioDto
    {
        [Required]
        [MinLength(6, ErrorMessage = "A minimum of 6 characters is necessary for the Username")]
        public string Username { get; set; } = null!;
        [Required]
        [MinLength(6, ErrorMessage = "A minimum of 6 characters is necessary for the Password")]
        public string Password { get; set; } = null!;
    }
}