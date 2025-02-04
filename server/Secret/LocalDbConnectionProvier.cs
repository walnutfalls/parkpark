using Microsoft.Extensions.Configuration;

namespace Secret
{
    public class LocalDbConnectionProvider : IConnectionProvider<string>
    {
        private IConfiguration _configuration;
        private string _secretKey;

        public LocalDbConnectionProvider(IConfiguration configuration, string secretKey)
        {
            _configuration = configuration;
            _secretKey = secretKey;
        }

        public string Connection => _configuration[_secretKey];
    }
}