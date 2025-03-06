using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/reportes")]
    [ApiController]
    [Authorize(Roles = "Administrador")] 
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("comerciantes")]
        public async Task<IActionResult> GenerarReporte()
        {
            var archivoStream = await _reporteService.GenerarReporteCSVAsync();
            return File(archivoStream, "text/csv", "Reporte_Comerciantes.csv");
            //return Ok(new { success = true, message = "Estado actualizado correctamente" });
        }
    }
}
