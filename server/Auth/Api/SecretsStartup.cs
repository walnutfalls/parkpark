using System.IO;
using Auth.Core.Interfaces;
using Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Secret;

namespace Api
{
    public static class SecretsStartup
    {
        public static void UseSecrets(this IServiceCollection services, IConfiguration config, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Development(services, env, config);
            }
            else
            {
                Release(services, env, config);
            }
        }

        private static void Development(IServiceCollection services, IHostingEnvironment environment, IConfiguration configuration)
        {
            var localEncryptionKeyProvider = new LocalEncryptionKeyProvider(configuration, "EncryptionKey");
            var localDbConnectionProvider = new LocalDbConnectionProvider(configuration, "ConnectionStrings:Auth");
            var localSmtpConnectionProvider = new LocalSmtpConnectionProvider(configuration, "MailServer");

            services.AddSingleton<IEncryptionKeyProvider>(localEncryptionKeyProvider);
            services.AddSingleton<IConnectionProvider<string>>(localDbConnectionProvider);
            services.AddSingleton<IConnectionProvider<SmtpServerConnection>>(localSmtpConnectionProvider);

            services.AddSingleton<IConfiguredSmtpClient>(new MailgunSmtpClient(localSmtpConnectionProvider.Connection));
        }

        private static void Release(IServiceCollection services, IHostingEnvironment environment, IConfiguration configuration)
        {            
            var connectionProvider = new EnvironmentVariableDbConnection(AuthEnvironmentVariable.AuthDbConnectionString);
            
            var smtpProvider = new EnvironmentVariableSmtpConnection(
                AuthEnvironmentVariable.SmtpHost, 
                AuthEnvironmentVariable.SmtpPort, 
                AuthEnvironmentVariable.SmtpUser,
                AuthEnvironmentVariable.SmtpPassword
            );

            var keyProvider = new EnvironmentVariableEncryptionKey(AuthEnvironmentVariable.AuthKeyFile);

            services.AddSingleton<IEncryptionKeyProvider>(keyProvider);
            services.AddSingleton<IConnectionProvider<string>>(connectionProvider);
            services.AddSingleton<IConnectionProvider<SmtpServerConnection>>(smtpProvider);

            services.AddSingleton<IConfiguredSmtpClient>(new MailgunSmtpClient(smtpProvider.Connection));
        }
    }
}