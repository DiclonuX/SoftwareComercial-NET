using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services
{
    internal class ComercianteService : IComercianteService
    {
        private readonly IComercianteRepository _repository;

        public ComercianteService(IComercianteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Comerciante?> ObtenerPorIdAsync(int id) => await _repository.ObtenerPorIdAsync(id);

        public async Task<List<Comerciante>> ObtenerTodosAsync(int pagina, int tamanoPagina, string? filtro, DateTime? fechaRegistro, bool? estado) =>
            await _repository.ObtenerTodosAsync(pagina, tamanoPagina, filtro, fechaRegistro, estado);

        public async Task<Comerciante> CrearAsync(ComercianteDTO dto, int usuarioId)
        {
            var comerciante = new Comerciante
            {
                NombreRazonSocial = dto.NombreRazonSocial,
                Telefono = dto.Telefono,
                CorreoElectronico = dto.CorreoElectronico,
                FechaRegistro = DateTime.UtcNow,
                Estado = dto.Estado,
                FechaActualizacion = DateTime.UtcNow,
                IdMunicipio = dto.IdMunicipio,
                UsuarioModificacionId = usuarioId
            };

            return await _repository.CrearAsync(comerciante);
        }

        public async Task<bool> ActualizarAsync(int id, ComercianteDTO dto, int usuarioId)
        {
            var comerciante = await _repository.ObtenerPorIdAsync(id);
            if (comerciante == null) return false;

            comerciante.NombreRazonSocial = dto.NombreRazonSocial;
            comerciante.Telefono = dto.Telefono;
            comerciante.CorreoElectronico = dto.CorreoElectronico;
            comerciante.Estado = dto.Estado;
            comerciante.FechaActualizacion = DateTime.UtcNow;
            comerciante.IdMunicipio = dto.IdMunicipio; 
            comerciante.UsuarioModificacionId = usuarioId;

            return await _repository.ActualizarAsync(comerciante);
        }

        public async Task<bool> EliminarAsync(int id) => await _repository.EliminarAsync(id);

        public async Task<bool> ModificarEstadoAsync(int id) => await _repository.ModificarEstadoAsync(id);
    }
}
