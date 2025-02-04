using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Secret;

namespace Api
{
    public static class JwtStartup
    {

        public static void UseJwt(this IServiceCollection services)
        {
            // Build an intermediate service provider
            var sp = services.BuildServiceProvider();
            var keyProvider = sp.GetService<IEncryptionKeyProvider>();


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyProvider.EncryptionKeyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}