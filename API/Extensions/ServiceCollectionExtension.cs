using API.Core.Interfaces;
using API.Core.Services;
using API.Infrastructure.Repositories;

namespace API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection addServices(this IServiceCollection services)
        {
            services.AddSingleton<JwtService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
            services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
            services.AddSingleton<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
