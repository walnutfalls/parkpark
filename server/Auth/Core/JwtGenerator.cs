using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Auth.Core.Interfaces;

using Auth.Core.Models;

using Secret;

namespace Auth.Core
{
    public class JwtGenerator : IJwtGenerator
    {
        private IEncryptionKeyProvider _keyProvider;
        private IConfiguration _configuration;

        public JwtGenerator(IEncryptionKeyProvider keyProvider, IConfiguration configuration)
        {
            _keyProvider = keyProvider;
            _configuration = configuration;
        }

        public string GetToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(_keyProvider.EncryptionKeyBytes);
            var domain = _configuration["Domain"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}