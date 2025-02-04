using System;

namespace Secret
{
    public class EnvironmentVariableSmtpConnection : IConnectionProvider<SmtpServerConnection>
    {
        private string _hostKey;
        private string _portKey;
        private string _userKey;
        private string _passwordKey;

        public EnvironmentVariableSmtpConnection(
            string hostKey,
            string portKey,
            string userKey,
            string passwordKey)
        {
            _hostKey = hostKey;
            _portKey = portKey;
            _userKey = userKey;
            _passwordKey = passwordKey;
        }


        public SmtpServerConnection Connection => new SmtpServerConnection
        {
            Host = Environment.GetEnvironmentVariable(_hostKey),
            Port = int.Parse(Environment.GetEnvironmentVariable(_portKey)),
            User = Environment.GetEnvironmentVariable(_userKey),
            Password = Environment.GetEnvironmentVariable(_passwordKey)
        };
    }
}