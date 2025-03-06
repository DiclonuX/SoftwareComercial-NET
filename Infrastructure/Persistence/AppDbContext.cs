using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Comerciante> Comerciantes { get; set; }
        public DbSet<Establecimiento> Establecimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
                   new Rol { Id = 1, Nombre = "Administrador" },
                   new Rol { Id = 2, Nombre = "AuxiliarRegistro" }
            );

            modelBuilder.Entity<Municipio>().HasData(
                new Municipio { Id = 1, Nombre = "Bogotá" },
                new Municipio { Id = 2, Nombre = "Medellín" },
                new Municipio { Id = 3, Nombre = "Cali" }
            );

            modelBuilder.Entity<Usuario>().HasOne(u => u.Rol)
           .WithMany()
           .HasForeignKey(u => u.IdRol);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.CorreoElectronico).IsUnique();
            modelBuilder.Entity<Comerciante>().HasOne<Usuario>(c => c.UsuarioModificacion)
                .WithMany().HasForeignKey(c => c.UsuarioModificacionId);
            modelBuilder.Entity<Establecimiento>().HasOne<Comerciante>(e => e.Comerciante)
                .WithMany().HasForeignKey(e => e.IdComerciante);
        }
    }
}
