using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Secret
{
    public class EnvironmentVariableDbConnection : IConnectionProvider<string>
    {        
        private string _secretKey;

        public EnvironmentVariableDbConnection(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string Connection => Environment.GetEnvironmentVariable(_secretKey);
    }
}