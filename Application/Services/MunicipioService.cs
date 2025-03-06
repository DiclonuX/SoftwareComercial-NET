using Application.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services
{
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioRepository _repository;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "ListaMunicipios";

        public MunicipioService(IMunicipioRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<List<Municipio>> ObtenerTodosAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out List<Municipio>? municipios))
            {
                municipios = await _repository.ObtenerTodosAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(CacheKey, municipios, cacheOptions);
            }
            return municipios!;
        }
    }
}
