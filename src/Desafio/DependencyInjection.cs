using Desafio.Backend.Application;
using Desafio.Backend.Domain;
using Desafio.Backend.Infra.Repositories;

namespace Desafio
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
