using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Auth.Core.Interfaces
{
    public interface IConfiguredSmtpClient
	{
		Task Send(MailMessage message);
	}
}
