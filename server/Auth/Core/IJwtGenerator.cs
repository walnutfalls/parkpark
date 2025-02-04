using System.Collections.Generic;
using System.Security.Claims;
using Auth.Core.Models;

namespace Auth.Core
{
    public interface IJwtGenerator
    {
        string GetToken(IEnumerable<Claim> claims);
    }
}