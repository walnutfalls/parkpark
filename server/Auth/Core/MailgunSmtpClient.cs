using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Auth.Core.Interfaces;
using Secret;

namespace Core
{
   
    public class MailgunSmtpClient : IConfiguredSmtpClient
    {       
        private SmtpClient _client;

        public MailgunSmtpClient(SmtpServerConnection connection)
        {
            _client = BuildClient(connection);
        }

        public async Task Send(MailMessage message)
        {
            await _client.SendMailAsync(message);
        }

        private SmtpClient BuildClient(SmtpServerConnection connection)
        {
            SmtpClient client = new SmtpClient(connection.Host, connection.Port);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(connection.User, connection.Password);
            return client;
        }
    }
}