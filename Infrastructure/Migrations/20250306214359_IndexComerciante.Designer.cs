﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250306214359_IndexComerciante")]
    partial class IndexComerciante
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Domain.Entities.Comerciante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdMunicipio")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MunicipioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreRazonSocial")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UsuarioModificacionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("UsuarioModificacionId");

                    b.ToTable("Comerciantes");
                });

            modelBuilder.Entity("Domain.Entities.Establecimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ComercianteId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdComerciante")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Ingresos")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumeroEmpleados")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UsuarioModificacionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComercianteId");

                    b.HasIndex("IdComerciante");

                    b.ToTable("Establecimientos");
                });

            modelBuilder.Entity("Domain.Entities.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Municipios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Bogotá"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Medellín"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Cali"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "AuxiliarRegistro"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdRol")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CorreoElectronico")
                        .IsUnique();

                    b.HasIndex("IdRol");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Domain.Entities.Comerciante", b =>
                {
                    b.HasOne("Domain.Entities.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId");

                    b.HasOne("Domain.Entities.Usuario", "UsuarioModificacion")
                        .WithMany()
                        .HasForeignKey("UsuarioModificacionId");

                    b.Navigation("Municipio");

                    b.Navigation("UsuarioModificacion");
                });

            modelBuilder.Entity("Domain.Entities.Establecimiento", b =>
                {
                    b.HasOne("Domain.Entities.Comerciante", null)
                        .WithMany("Establecimientos")
                        .HasForeignKey("ComercianteId");

                    b.HasOne("Domain.Entities.Comerciante", "Comerciante")
                        .WithMany()
                        .HasForeignKey("IdComerciante")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comerciante");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Domain.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Domain.Entities.Comerciante", b =>
                {
                    b.Navigation("Establecimientos");
                });
#pragma warning restore 612, 618
        }
    }
}
