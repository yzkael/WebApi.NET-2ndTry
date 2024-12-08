using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string Carnet { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        //One to One relationShip

        public string? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

    }
}