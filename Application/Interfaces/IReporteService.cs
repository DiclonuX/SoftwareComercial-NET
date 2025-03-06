using System.IO;

namespace Application.Interfaces
{
    public interface IReporteService
    {
        Task<MemoryStream> GenerarReporteCSVAsync();
    }
}
