using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.Roles
{
    public class CreateRolesRequestDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "El Rol debe tener al menos 4 caracteres")]
        public string Role { get; set; } = null!;
    }
}