using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/municipios")]
    [ApiController]
    [Authorize] // 🔒 Requiere autenticación con JWT
    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var municipios = await _municipioService.ObtenerTodosAsync();
            return Ok(new { success = true, data = municipios });
        }
    }
}
