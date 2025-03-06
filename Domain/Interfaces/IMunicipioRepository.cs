using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMunicipioRepository
    {
        Task<List<Municipio>> ObtenerTodosAsync();
    }
}
