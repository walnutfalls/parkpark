using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Auth.Core.Models;
using System;
using Auth.Core.Repositories.Interface;
using System.Threading.Tasks;


namespace Auth.Core
{
    public class Login : ILogin
    {        
        private IUserRepository _userRepository;
        private IJwtGenerator _jwtGenerator;

        public Login(IJwtGenerator jwtGenerator, IUserRepository userRepository)
        {
            _jwtGenerator = jwtGenerator;
            _userRepository = userRepository;
        }

        public async Task<bool> AreCredentialsValid(Credentials credentials)
        {
            var user = await _userRepository.GetUserByHandle(credentials.Handle);
			if (user == null) return false;

            var pw = new Password(user.Password, new Salt(user.Salt));
            return pw.Check(credentials.Password);
        }

        public string CreateToken(Credentials credentials, string domain)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, credentials.Handle)
            };

            return _jwtGenerator.GetToken(claims);
        }
    }
}