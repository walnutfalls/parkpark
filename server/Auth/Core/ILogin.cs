using System.Threading.Tasks;
using Auth.Core.Models;

namespace Auth.Core
{
    public interface ILogin
    {
        Task <bool> AreCredentialsValid(Credentials credentials);
        string CreateToken(Credentials credentials, string domain);
    }
}