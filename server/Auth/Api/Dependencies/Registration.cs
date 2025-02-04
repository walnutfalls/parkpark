using Microsoft.Extensions.DependencyInjection;
using Auth.Core.Interfaces;
using Auth.Core;

namespace Auth.Api.Dependencies
{
    public static class RegistrationDiExtensions
    {
        public static void InjectRegistration(this IServiceCollection services)
        {
            services.AddSingleton<IRegistration, Registration>();            
        }
    }
}