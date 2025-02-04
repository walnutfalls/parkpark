using System.Net.Mail;
using System.Threading.Tasks;
using Auth.Core.Models;

using Auth.Db.Models;
using Auth.Core.Repositories.Interface;
using System.Linq;
using System;

using Auth.Core.Interfaces;

using User = Auth.Db.Models.User;
using Auth.Core.Exceptions;

namespace Auth.Core
{
    public class Registration : IRegistration
    {
        private IUserRepository _userRepository;

        private IConfiguredSmtpClient _configuredSmtpClient;

        public Registration(
            IUserRepository userRepository,
            IConfiguredSmtpClient emailConfigProvider)
        {
            _userRepository = userRepository;
            _configuredSmtpClient = emailConfigProvider;
        }

        public async Task Register(RegistrationModel model, Uri verifyEmailUriBase)
        {
            var salt = new Salt();
            var pw = new Password(model.Password, salt);
            var user = new User
            {
                Handle = model.Handle,
                Password = pw.ProtectedForm,
                Salt = salt.Data,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
            };

            try
            {
                await _userRepository.InsertNewUser(user);
                await SendVerificationEmail(user, verifyEmailUriBase);
            }
            catch (SmtpException ex)
            {
				await _userRepository.DeleteUser(user);
                Console.WriteLine(ex.Message + "\n\n" + ex?.InnerException?.Message);
                throw new AuthenticationException("Could not register.");
            }
			catch (DataAccessException dae)
			{
				Console.WriteLine(dae.Message + "\n\n" + dae?.InnerException?.Message);
                throw new AuthenticationException("Could not register.");
			}
        }

        public async Task<bool> VerifyEmail(string verificationTokenId, int userId)
        {
            if (!_userRepository.IsUserVerified(userId))
            {
                throw new AuthenticationException();
            }

            await _userRepository.DeleteVerificationToken(verificationTokenId);
            return true;
        }



        private async Task SendVerificationEmail(User user, Uri verificationLinkBase)
        {
            var verificationToken = user.VerificationTokens.First();
            var verifyLink = WithTokenParams(verificationLinkBase, user, verificationToken);

            var subject = "Verification Email";
            var body = $"Please verify your account here: {verifyLink.AbsoluteUri}";

            var message = new MailMessage("parkparkverif@gmail.com", user.Email, subject, body);
            await _configuredSmtpClient.Send(message);
        }

        private Uri WithTokenParams(Uri uri, User user, VerificationToken verificationToken)
        {
            return new Uri(uri.AbsoluteUri + $"?verificationTokenId={verificationToken.VerificationTokenId}&userId={user.UserId}");
        }
    }
}
