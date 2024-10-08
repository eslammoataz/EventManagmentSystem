using EventManagmentSystem.Application.Services.Auth;
using EventManagmentSystem.Application.Services.EmailService;
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
            services.AddSingleton<IEmailService, GmailEmailService>();

            return services;
        }
    }
}
