using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class KeyManagerStartup
    {
        public static void UseKeyManager(this IServiceCollection services, IConfiguration config)
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(config["KeyDir"]));
        }
    }
}