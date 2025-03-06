using Application.DTOs;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IComercianteService
    {
        Task<Comerciante?> ObtenerPorIdAsync(int id);
        Task<List<Comerciante>> ObtenerTodosAsync(int pagina, int tamanoPagina, string? filtro, DateTime? fechaRegistro, bool? estado);
        Task<Comerciante> CrearAsync(ComercianteDTO dto, int usuarioId);
        Task<bool> ActualizarAsync(int id, ComercianteDTO dto, int usuarioId);
        Task<bool> EliminarAsync(int id);
        Task<bool> ModificarEstadoAsync(int id);
    }
}
