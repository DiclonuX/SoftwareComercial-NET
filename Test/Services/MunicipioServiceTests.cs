using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Test.Services
{
    [TestFixture]
    public class MunicipioServiceTests
    {
        private IMunicipioService _municipioService;
        private Mock<IMunicipioRepository> _repositoryMock;
        private IMemoryCache _cache;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IMunicipioRepository>();
            _cache = new MemoryCache(new MemoryCacheOptions());

            _repositoryMock.Setup(repo => repo.ObtenerTodosAsync())
                .ReturnsAsync(new List<Municipio>
                {
            new Municipio { Id = 1, Nombre = "Bogotá" },
            new Municipio { Id = 2, Nombre = "Medellín" }
                });

            _municipioService = new MunicipioService(_repositoryMock.Object, _cache);
        }

        [TearDown]
        public void TearDown()
        {
            _cache.Dispose();
        }

        [Test]
        public async Task ObtenerTodosAsync_ShouldReturnMunicipios()
        {
            // Act
            var result = await _municipioService.ObtenerTodosAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
