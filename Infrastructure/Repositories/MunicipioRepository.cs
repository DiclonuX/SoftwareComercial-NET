using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly AppDbContext _context;

        public MunicipioRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Municipio>> ObtenerTodosAsync()
        {
            return await _context.Municipios.AsNoTracking().ToListAsync();
        }
    }
}
