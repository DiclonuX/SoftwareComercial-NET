using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly AppDbContext _context;

        public ReporteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReporteComerciante>> ObtenerDatosReporteAsync()
        {
            return await _context.Database
                .SqlQueryRaw<ReporteComerciante>("EXEC ObtenerComerciantesActivos")
                .ToListAsync();
        }
    }
}
