using System;

namespace Secret
{
    public enum SecretType
    {
        Key = 1,
        
    }
    
    public class SecretRetrievalException : Exception
    {
        public SecretType _secretKey;

        public SecretRetrievalException(SecretType keyType, string message) : base(message)
        {
            _secretKey = keyType;
        }
    }
}