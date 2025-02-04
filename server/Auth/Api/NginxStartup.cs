using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class NginxStartup
    {
        public static void UseNginx(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }

        public static void UseNginx(this IServiceCollection services, IConfiguration config)
        {
            var loadBalancerIp = IPAddress.Parse(config["LoadBalancerIp"]);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(loadBalancerIp);
            });
        }
    }
}