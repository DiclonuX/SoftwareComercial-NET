using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: false),
                    Contrasena = table.Column<string>(type: "TEXT", nullable: false),
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comerciantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreRazonSocial = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IdMunicipio = table.Column<int>(type: "INTEGER", nullable: false),
                    MunicipioId = table.Column<int>(type: "INTEGER", nullable: true),
                    UsuarioModificacionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comerciantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comerciantes_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comerciantes_Usuarios_UsuarioModificacionId",
                        column: x => x.UsuarioModificacionId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Establecimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdComerciante = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Ingresos = table.Column<decimal>(type: "TEXT", nullable: false),
                    NumeroEmpleados = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UsuarioModificacionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ComercianteId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establecimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Establecimientos_Comerciantes_ComercianteId",
                        column: x => x.ComercianteId,
                        principalTable: "Comerciantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Establecimientos_Comerciantes_IdComerciante",
                        column: x => x.IdComerciante,
                        principalTable: "Comerciantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Municipios",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Bogotá" },
                    { 2, "Medellín" },
                    { 3, "Cali" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "AuxiliarRegistro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comerciantes_MunicipioId",
                table: "Comerciantes",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Comerciantes_UsuarioModificacionId",
                table: "Comerciantes",
                column: "UsuarioModificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Establecimientos_ComercianteId",
                table: "Establecimientos",
                column: "ComercianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Establecimientos_IdComerciante",
                table: "Establecimientos",
                column: "IdComerciante");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CorreoElectronico",
                table: "Usuarios",
                column: "CorreoElectronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Establecimientos");

            migrationBuilder.DropTable(
                name: "Comerciantes");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
