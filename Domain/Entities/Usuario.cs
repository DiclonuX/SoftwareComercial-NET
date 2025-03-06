using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string CorreoElectronico { get; set; } = string.Empty;
        public required string Contrasena { get; set; } = string.Empty;
        public required int IdRol { get; set; }
        public Rol? Rol { get; set; }
    }
}
