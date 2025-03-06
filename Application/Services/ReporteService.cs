using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<MemoryStream> GenerarReporteCSVAsync()
        {
            var datos = await _reporteRepository.ObtenerDatosReporteAsync();

            var comerciantes = datos.Select(c => new ReporteComercianteDTO
            {
                NombreRazonSocial = c.NombreRazonSocial,
                Municipio = c.Municipio,
                Telefono = c.Telefono,
                CorreoElectronico = c.CorreoElectronico,
                FechaRegistro = c.FechaRegistro,
                Estado = c.Estado,
                CantidadEstablecimientos = c.CantidadEstablecimientos,
                TotalIngresos = c.TotalIngresos,
                CantidadEmpleados = c.CantidadEmpleados
            }).ToList();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Nombre o Razón Social|Municipio|Teléfono|Correo Electrónico|Fecha de Registro|Estado|Cantidad Establecimientos|Total Ingresos|Cantidad Empleados");

            foreach (var c in comerciantes)
            {
                csvBuilder.AppendLine(
                    $"{c.NombreRazonSocial}|{c.Municipio}|{c.Telefono ?? "N/A"}|{c.CorreoElectronico ?? "N/A"}|" +
                    $"{c.FechaRegistro.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}|{c.Estado}|" +
                    $"{c.CantidadEstablecimientos}|{c.TotalIngresos.ToString("F2", CultureInfo.InvariantCulture)}|{c.CantidadEmpleados}"
                );
            }

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvBuilder.ToString()));
            return memoryStream;
        }
    }
}
