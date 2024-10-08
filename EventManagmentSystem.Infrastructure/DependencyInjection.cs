using EventManagmentSystem.Application;
using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Infrastructure.Data;
using EventManagmentSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagmentSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureProjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["DATABASE_HOST"];
            var port = configuration["DATABASE_PORT"];
            var name = configuration["DATABASE_NAME"];
            var username = configuration["DATABASE_USERNAME"];
            var password = configuration["DATABASE_PASSWORD"];
            var connectionString =
                $"Server={host};Port={port};Database={name};User Id={username};Password={password};";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("EventManagmentSystem.Infrastructure")));



            // Register repositories
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            // Register Unit of Work


            return services;
        }
    }
}
