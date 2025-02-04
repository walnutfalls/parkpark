using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Db.Models
{
    public class VerificationToken
    {        
        public string VerificationTokenId { get; set; }

        public int UserId {get; set;}
        public User User {get; set;}
    }

    internal class VerificationTokenTableConfig
    {
        public static Action<EntityTypeBuilder<VerificationToken>> Customize = (vt) =>
        {
            vt.Property(t => t.VerificationTokenId).HasDefaultValueSql(SqlValue.RandomText);
        };
    }
}