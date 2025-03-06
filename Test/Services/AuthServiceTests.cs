using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Application.Services;
using Application.DTOs;
using Infrastructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Test.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AuthService _authService;
        private Mock<IConfiguration> _configMock;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Simular configuración de JWT
            var configValues = new Dictionary<string, string>
            {
                { "JwtSettings:Secret", "ClaveUltraSeguraParaJwt123!" },
                { "JwtSettings:Issuer", "ComercioApp" },
                { "JwtSettings:Audience", "ComercioAppUsers" }
            };
            _configMock = new Mock<IConfiguration>();
            foreach (var kvp in configValues)
                _configMock.Setup(x => x[kvp.Key]).Returns(kvp.Value);

            // Configurar base de datos en memoria
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            _context = new AppDbContext(options);

            // Insertar datos de prueba
            _context.Roles.Add(new Rol { Id = 1, Nombre = "Administrador" });
            _context.Usuarios.Add(new Usuario
            {
                Id = 1,
                Nombre = "Test User",
                CorreoElectronico = "test@comercio.com",
                Contrasena = BCrypt.Net.BCrypt.HashPassword("password123"),
                IdRol = 1
            });
            _context.SaveChanges();

            _authService = new AuthService(_context, _configMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose(); // Liberar los recursos de la base de datos en memoria
        }

        [Test]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var request = new AuthRequestDTO { CorreoElectronico = "test@comercio.com", Contrasena = "password123" };

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.Token);
            Assert.AreEqual("Test User", result.Nombre);
            Assert.AreEqual("Administrador", result.Rol);
        }

        [Test]
        public async Task LoginAsync_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var request = new AuthRequestDTO { CorreoElectronico = "test@comercio.com", Contrasena = "wrongpassword" };

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.IsNull(result);
        }
    }
}
