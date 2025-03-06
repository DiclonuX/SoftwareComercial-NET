using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comerciante
    {
        [Key]
        public int Id { get; set; }
        public required string NombreRazonSocial { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public required DateTime FechaRegistro { get; set; }
        public required bool Estado { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public required int IdMunicipio { get; set; }
        public Municipio? Municipio { get; set; }
        public int? UsuarioModificacionId { get; set; }
        public Usuario? UsuarioModificacion { get; set; }
        public ICollection<Establecimiento>? Establecimientos { get; set; }
    }
}
