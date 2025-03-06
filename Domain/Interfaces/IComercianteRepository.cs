using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IComercianteRepository
    {
        Task<Comerciante?> ObtenerPorIdAsync(int id);
        Task<List<Comerciante>> ObtenerTodosAsync(int pagina, int tamanoPagina, string? filtro, DateTime? fechaRegistro, bool? estado);
        Task<Comerciante> CrearAsync(Comerciante comerciante);
        Task<bool> ActualizarAsync(Comerciante comerciante);
        Task<bool> EliminarAsync(int id);
        Task<bool> ModificarEstadoAsync(int id);
   
    }
}
