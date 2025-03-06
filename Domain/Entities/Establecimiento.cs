using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Establecimiento
    {
        [Key]
        public int Id { get; set; }
        public int IdComerciante { get; set; }
        public required string Nombre { get; set; }
        public required decimal Ingresos { get; set; }
        public required int NumeroEmpleados { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? UsuarioModificacionId { get; set; }
        public Comerciante Comerciante { get; set; } = null!;
    }
}
