using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Auth.Core.Interfaces;
using Auth.Core;

namespace Auth.Api.Dependencies
{
    public static class LoginDiExtensions
    {
        public static void InjectLogin(this IServiceCollection services, IHostingEnvironment environment)
        {
            services.AddSingleton<ILogin, Auth.Core.Login>();
        }
    }
}