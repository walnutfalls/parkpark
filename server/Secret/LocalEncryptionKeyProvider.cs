using System.Text;
using Microsoft.Extensions.Configuration;

namespace Secret
{
    public class LocalEncryptionKeyProvider : IEncryptionKeyProvider
    {
        private IConfiguration _configuration;
        private string _secretKey;

        public LocalEncryptionKeyProvider(IConfiguration configuration, string secretKey)
        {
            _configuration = configuration;
            _secretKey = secretKey;
        }

        public string EncryptionKey => _configuration[_secretKey];

        public byte[] EncryptionKeyBytes => Encoding.ASCII.GetBytes(EncryptionKey);
    }
}