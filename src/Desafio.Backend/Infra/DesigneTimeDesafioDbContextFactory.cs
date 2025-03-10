using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Infra
{
    public class DesigneTimeDesafioDbContextFactory : IDesignTimeDbContextFactory<DesafioDbContext>
    {
        public DesafioDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Backend.json")
                .Build();

            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DesafioDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DesafioDbContext(optionsBuilder.Options);
        }
    }
}
