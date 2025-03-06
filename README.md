# NET
# Proyecto de Net en Visual Studio

# Introducción

Este proyecto consite en una API REST desarrollada en .NET 8 que permite gestionar comerciantes y sus establecimientos.
El proyecto cuenta con autenticación basada en JWT, validaciones avanzadas y un sistema de logs con Serilog.

![image](https://github.com/user-attachments/assets/4b505a75-77cc-4637-9d34-65c53eb78955)


# Tecnologías Utilizadas

Backend: .NET 8, ASP.NET Core Web API

Base de Datos: SQL Server, SQLite (según configuración)

ORM: Entity Framework Core 8

Autenticación: JWT

Validaciones: FluentValidation

Logging: Serilog (con almacenamiento en SQL Server o archivos locales)

Pruebas Unitarias: NUnit, Moq

Documentación API: Swagger

Contenedores: Docker

# Arquitectura Aplicada

Clean Architecture con separación en capas:

Domain (Entidades y Repositorios)

Application (Servicios, DTOs, Validaciones)

Infrastructure (Persistencia y acceso a datos)

API (Controladores y configuración API)

# Principios SOLID y Buenas Prácticas:

Inyección de dependencias

Uso de interfaces para abstracción (IRepository, IService)

Manejo de errores con Middleware

Indexación en base de datos para optimizar consultas

# Configuración del Proyecto

Prerrequisitos

Antes de ejecutar el proyecto, asegúrate de tener instalados:

.NET SDK 8

SQL Server o SQLite

Visual Studio 2022+ o VS Code

Docker

# Clonar el Proyecto
```sh
git clone https://github.com/DiclonuX/SoftwareComercial-NET.git
cd SoftwareComercial-NET
```
# Configurar el appsettings.json
```json
{
    "JwtSettings": {
    "Secret": "SoftwareComercial123*",
    "Issuer": "SoftwareComercial",
    "Audience": "SoftwareComercial",
    "ExpirationMinutes": 60
  },
  "DatabaseProvider": "SQLite", // Cambiar a "SQLite" si es necesario
  "ConnectionStrings": {
    "SQLiteConnection": "Data Source=ComercioDB.sqlite;", // Cambiar "ComercioDB" por el nombre de la base SQLite que quieres.
    "SqlServerConnection": "Server=localhost;Database=ComercioDB;User Id=sa;Password=TuPasswordSegura;" // Cambiar "ComercioDB" por el nombre de la base SQLServer que quieres.
  },
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log.txt", 
          "rollingInterval": "Day"
        }
      }
    ]
  }
}
```
Si usas Secrets en Visual Studio, configura los valores con:
```sh
dotnet user-secrets set "JwtSettings:Secret" "ClaveUltraSegura"
dotnet user-secrets set "ConnectionStrings:SqlServerConnection" "Server=localhost;Database=ComercioDB;User Id=sa;Password=TuPasswordSegura;"
```
# Aplicar Migraciones y Crear la Base de Datos
```sh
dotnet ef database update --project Infrastructure --startup-project API
```
Esto creará las tablas en la base de datos.

# Ejecutar el Proyecto
```sh
dotnet run --project API
```

![image](https://github.com/user-attachments/assets/186ed56a-5923-4a3d-8948-b12e6cb08225)


# Pruebas Unitarias

Ejecuta las pruebas con:
```sh
dotnet test
```
Esto ejecutará las pruebas con NUnit.

# Contacto

Nombre: Servio Lemos

Correo: servio.lemos@gmail.com

GitHub: [Repositorio del Proyecto](https://github.com/DiclonuX)
