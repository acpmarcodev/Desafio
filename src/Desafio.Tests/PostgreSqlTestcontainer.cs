using Desafio.Backend.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;

namespace Desafio.Tests
{
    public class PostgreSqlTestcontainer : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgresContainer;

        public PostgreSqlTestcontainer()
        {
            _postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")  
                .WithDatabase("testdb2")
                .WithUsername("testuser")
                .WithPassword("testpass")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _postgresContainer.StartAsync();
            var ConnectionString = _postgresContainer.GetConnectionString();

            var options = new DbContextOptionsBuilder<DesafioDbContext>()
                .UseNpgsql(ConnectionString)
            .Options;

            using var context = new DesafioDbContext(options);
            await context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await _postgresContainer.DisposeAsync();
        }

        public string GetConnectionString()
        {
            return _postgresContainer.GetConnectionString();
        }

        [Fact]
        public void TestDatabaseConnection()
        {
            string connectionString = _postgresContainer.GetConnectionString();
            Assert.NotNull(connectionString);
        }
    }


}

