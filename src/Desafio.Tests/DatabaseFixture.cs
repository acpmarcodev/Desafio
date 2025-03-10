using Desafio.Backend.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace Desafio.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        public readonly DesafioDbContext _dbContext;
        private readonly PostgreSqlTestcontainer _postgresqlContainer;

        public DatabaseFixture()
        {
            //base real
            //var configuration = new ConfigurationBuilder()
            //             .AddJsonFile("appsettings.Test.json")
            //             .Build();

            //var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

            //var options = new DbContextOptionsBuilder<DesafioDbContext>()
            //    .UseNpgsql(connectionString)
            //    .Options;

            //base docker
            _postgresqlContainer = new PostgreSqlTestcontainer();
             
            _postgresqlContainer.InitializeAsync().Wait();

            var connectionString = _postgresqlContainer.GetConnectionString();

            var options = new DbContextOptionsBuilder<DesafioDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            _dbContext = new DesafioDbContext(options);
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _postgresqlContainer.DisposeAsync();
        }
    }

}
