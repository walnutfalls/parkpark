using System;
using System.IO;
using System.Text;
using Utils;

namespace Secret
{
    public class EnvironmentVariableEncryptionKey : IEncryptionKeyProvider
    {
        private string _secretKey;

        public EnvironmentVariableEncryptionKey(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string EncryptionKey
        {
            get
            {
                var fileName = Environment.GetEnvironmentVariable(_secretKey);
                var fn = fileName.ResolveUnixHome();
                var file = File.ReadAllText(fileName.ResolveUnixHome());
                return file;
            }
        }

        public byte[] EncryptionKeyBytes => Encoding.ASCII.GetBytes(EncryptionKey);
    }
}