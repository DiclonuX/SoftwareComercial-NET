using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var databaseProvider = configuration["DatabaseProvider"];

            var connectionString = databaseProvider == "SQLite"
                ? configuration.GetConnectionString("SQLiteConnection")
                : configuration.GetConnectionString("SqlServerConnection");

            if (databaseProvider == "SQLite")
                optionsBuilder.UseSqlite(connectionString);
            else
                optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
