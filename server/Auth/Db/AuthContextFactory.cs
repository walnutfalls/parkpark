using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auth.Db
{
    public class PostgresAuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var env = new Environment();
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();

            System.Console.WriteLine("Creating context..");

            switch (env.Type)
            {
                case Environment.EnvironmentType.Development:
                    optionsBuilder.UseNpgsql("Host=142.93.245.92;Database=auth;Username=postgres;Password=5UnsafeDevelopmentPassword$");
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new AuthContext(optionsBuilder.Options);
        }
    }

}