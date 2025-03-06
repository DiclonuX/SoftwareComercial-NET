using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private static Dictionary<string, int> intentosFallidos = new();
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDTO request)
        {
            if (intentosFallidos.ContainsKey(request.CorreoElectronico) && intentosFallidos[request.CorreoElectronico] >= 5)
            {
                return Unauthorized(new { success = false, message = "Demasiados intentos fallidos. Intenta más tarde." });
            }

            var response = await _authService.LoginAsync(request);
            if (response == null)
            {
                intentosFallidos[request.CorreoElectronico] = intentosFallidos.GetValueOrDefault(request.CorreoElectronico, 0) + 1;
                return Unauthorized(new { success = false, message = "Credenciales incorrectas" });

            }
            intentosFallidos.Remove(request.CorreoElectronico);
            return Ok(new { success = true, data = response });
        }
    }
}
