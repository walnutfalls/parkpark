using Auth.Core;
using Auth.Core.Repositories;
using Auth.Core.Repositories.Interface;
using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Api.Dependencies
{

    public static class InjectAppDependenciesExtensions
    {
        public static void InjectAppDependencies(
            this IServiceCollection services, 
            IHostingEnvironment environment, 
            IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IJwtGenerator, JwtGenerator>();

            services.InjectLogin(environment);
            services.InjectRegistration();
        }   
    }
}