using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReporteComerciante
    {
        public string? NombreRazonSocial { get; set; }
        public string? Municipio { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public int CantidadEstablecimientos { get; set; }
        public decimal TotalIngresos { get; set; }
        public int CantidadEmpleados { get; set; }
    }
}
