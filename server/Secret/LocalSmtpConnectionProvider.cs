using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Secret
{
    public class LocalSmtpConnectionProvider : IConnectionProvider<SmtpServerConnection>
    {
         private IConfiguration _configuration;
        private string _secretKey;

        public LocalSmtpConnectionProvider(IConfiguration configuration, string secretKey)
        {
            _configuration = configuration;
            _secretKey = secretKey;
        }

        public SmtpServerConnection Connection => _configuration
            .GetSection(_secretKey)
            .Get<SmtpServerConnection>();
    }
}