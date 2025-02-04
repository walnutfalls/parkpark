using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Auth.Core.Exceptions;
using Auth.Db;

using Secret;

namespace Auth.Core.Repositories
{
    public abstract class EntityFrameworkRepositoryBase
    {
		private readonly IConnectionProvider<string> _databaseOptionsProvider;
		private readonly DbContextOptions<AuthContext> _options;

        public EntityFrameworkRepositoryBase(IConnectionProvider<string> optionsProvider)
		{
			_databaseOptionsProvider = optionsProvider;

            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();
            optionsBuilder.UseNpgsql(_databaseOptionsProvider.Connection);

            _options = optionsBuilder.Options;

		}
		
		protected AuthContext NewContext => new AuthContext(_options);

		protected async Task TryAsync(Func<Task> action)
		{
			try
			{
				await action();
			}
			catch (DbUpdateException ex)
			{
				Console.WriteLine("DB ERROR: " + ex.Message);
				throw new DataAccessException("DB ERROR", ex);
			}
		}

		public async Task TryInTransaction(Func<IDbContextTransaction, AuthContext,  Task> action)
		{
			using (var context = NewContext)
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					await TryAsync(async () =>
					{						
						await action(transaction, context);						
					});
				}
			}
			
		}
	}
}
