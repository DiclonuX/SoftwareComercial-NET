using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComercianteController : ControllerBase
    {
        private readonly IComercianteService _comercianteService;

        public ComercianteController(IComercianteService comercianteService)
        {
            _comercianteService = comercianteService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,AuxiliarRegistro")]
        public async Task<IActionResult> GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanoPagina = 5,
                                           [FromQuery] string? filtro = null, [FromQuery] DateTime? fechaRegistro = null, [FromQuery] bool? estado = null)
        {
            var comerciantes = await _comercianteService.ObtenerTodosAsync(pagina, tamanoPagina, filtro, fechaRegistro, estado);
            return Ok(new { success = true, data = comerciantes });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,AuxiliarRegistro")]
        public async Task<IActionResult> GetById(int id)
        {
            var comerciante = await _comercianteService.ObtenerPorIdAsync(id);
            if (comerciante == null) return NotFound(new { success = false, message = "Comerciante no encontrado" });
            return Ok(new { success = true, data = comerciante });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,AuxiliarRegistro")]
        public async Task<IActionResult> Create([FromBody] ComercianteDTO dto)
        {
            var validator = new ComercianteValidator();
            ValidationResult result = validator.Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(new { success = false, errors = result.Errors.Select(e => e.ErrorMessage) });
            }

            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var nuevoComerciante = await _comercianteService.CrearAsync(dto, usuarioId);
            return CreatedAtAction(nameof(GetById), new { id = nuevoComerciante.Id }, new { success = true, data = nuevoComerciante });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,AuxiliarRegistro")]
        public async Task<IActionResult> Update(int id, [FromBody] ComercianteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var actualizado = await _comercianteService.ActualizarAsync(id, dto, usuarioId);
            if (!actualizado) return NotFound(new { success = false, message = "Comerciante no encontrado" });

            return Ok(new { success = true, message = "Comerciante actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _comercianteService.EliminarAsync(id);
            if (!eliminado) return NotFound(new { success = false, message = "Comerciante no encontrado" });

            return Ok(new { success = true, message = "Comerciante eliminado correctamente" });
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = "Administrador,AuxiliarRegistro")]
        public async Task<IActionResult> ModificarEstado(int id)
        {
            var modificado = await _comercianteService.ModificarEstadoAsync(id);
            if (!modificado) return NotFound(new { success = false, message = "Comerciante no encontrado" });

            return Ok(new { success = true, message = "Estado actualizado correctamente" });
        }
    }
}
