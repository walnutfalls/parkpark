using Inflector;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Auth.Db.Models;


namespace Auth.Db
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
        { 

        }      

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<VerificationToken> VerificationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>(GroupTableConfig.Customize);
            builder.Entity<User>(UserTableConfig.Customize);
            builder.Entity<UserGroup>(UserGroupTableConfig.Customize);
            builder.Entity<VerificationToken>(VerificationTokenTableConfig.Customize);

            SetPostgresqlConventions(builder);           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        private void SetPostgresqlConventions(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase().Singularize();

                // Replace column names            
                foreach (var property in entity.GetProperties())
                    property.Relational().ColumnName = property.Name.ToSnakeCase();

                foreach (var key in entity.GetKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var key in entity.GetForeignKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var index in entity.GetIndexes())
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
            }
        }
    }
}
