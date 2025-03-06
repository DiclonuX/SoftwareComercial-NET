using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class ComercianteRepository : IComercianteRepository
    {
        private readonly AppDbContext _context;

        public ComercianteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comerciante?> ObtenerPorIdAsync(int id)
        {
            return await _context.Comerciantes.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comerciante>> ObtenerTodosAsync(int pagina, int tamanoPagina, string? filtro, DateTime? fechaRegistro, bool? estado)
        {
            var query = _context.Comerciantes.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
                query = query.Where(c => c.NombreRazonSocial.Contains(filtro));

            if (fechaRegistro.HasValue)
                query = query.Where(c => c.FechaRegistro.Date == fechaRegistro.Value.Date);

            if (estado.HasValue)
                query = query.Where(c => c.Estado == estado);

            return await query.Skip((pagina - 1) * tamanoPagina).Take(tamanoPagina).ToListAsync();
        }

        public async Task<Comerciante> CrearAsync(Comerciante comerciante)
        {
            _context.Comerciantes.Add(comerciante);
            await _context.SaveChangesAsync();
            return comerciante;
        }

        public async Task<bool> ActualizarAsync(Comerciante comerciante)
        {
            _context.Comerciantes.Update(comerciante);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var comerciante = await _context.Comerciantes.FindAsync(id);
            if (comerciante == null) return false;

            _context.Comerciantes.Remove(comerciante);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ModificarEstadoAsync(int id)
        {
            var comerciante = await _context.Comerciantes.FindAsync(id);
            if (comerciante == null) return false;

            comerciante.Estado = !comerciante.Estado;
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
