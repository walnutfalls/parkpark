using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Auth.Core.Exceptions;
using Auth.Core.Repositories.Interface;
using Auth.Db;
using Auth.Db.Models;

using Secret;

namespace Auth.Core.Repositories
{
    public class UserRepository : EntityFrameworkRepositoryBase, IUserRepository
    {
        public UserRepository(IConnectionProvider<string> optionsProvider) : base(optionsProvider)
        {
        }

        public async Task DeleteUser(User user)
        {
			using (var context = NewContext)
			{
				await TryAsync(() => {
					context.Users.Remove(user);
					return context.SaveChangesAsync();
				});
			}
        }

        public async Task DeleteVerificationToken(string verificationTokenId)
		{
			using (var context = NewContext)
			{
				context.VerificationTokens.Remove(new VerificationToken { VerificationTokenId = verificationTokenId });
				await context.SaveChangesAsync();
			}
		}

		public async Task<User> GetUserByHandle(string handle)
        {
            using (var context = NewContext)
            {
				return await context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Handle == handle);				
            }
        }
		public async Task<User> GetUserById(int userId)
		{
			using (var context = NewContext)
			{
				return await context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == userId);
			}
		}

		public async Task InsertNewUser(User user)
        {
            using (var context = NewContext)
            {
				await TryAsync(() => InsertUser(context, user));				
			}
        }

		public async Task InsertNewUser(User user, Action execInTransaction)
		{
			await TryInTransaction(async (trans, ctx) =>
			{
				var saving = InsertUser(ctx, user);
				execInTransaction();
				await saving;

				trans.Commit();
			});
		}

		public bool IsUserVerified(int userId)
		{
			using (var context = NewContext)
			{
				var hasToken = context.VerificationTokens.Any(t => t.UserId == userId);
				return hasToken;
			}
		}

		private Task<int> InsertUser(AuthContext context, User user)
		{
			var verificationToken = new VerificationToken { User = user };
			user.VerificationTokens.Add(verificationToken);
			context.Users.Add(user);
			context.VerificationTokens.Add(verificationToken);
			return context.SaveChangesAsync();
		}		
	}
}