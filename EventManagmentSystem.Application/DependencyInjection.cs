using EventManagmentSystem.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagmentSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationProjectDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //Register Services
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
