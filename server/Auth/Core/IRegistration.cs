using System;
using System.Threading.Tasks;

using Auth.Core.Models;

namespace Auth.Core
{
    public interface IRegistration
    {
        Task Register(RegistrationModel model, Uri verifyEmailUri);
		Task<bool> VerifyEmail(string verificationTokenId, int userId);
    }
}