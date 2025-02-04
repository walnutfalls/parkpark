using System;
using System.Threading.Tasks;
using Auth.Db.Models;

namespace Auth.Core.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByHandle(string handle);
		Task<User> GetUserById(int userId);


		Task InsertNewUser(User user);
		Task InsertNewUser(User user, Action execInTransaction);

		Task DeleteUser(User user);

		bool IsUserVerified(int userId);
		Task DeleteVerificationToken(string verificationTokenId);
	}
}