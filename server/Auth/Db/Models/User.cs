using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Db.Models
{
    public class User
    {
		public User()
		{
			VerificationTokens = new List<VerificationToken>();
		}

		[Key]
        public int UserId { get; set; }
        
        [MaxLength(25)]
        public string Handle { get; set; }
        
        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
        
        [MaxLength(1024)]
        public string AvatarUrl { get; set; }

        public ICollection<UserGroup> UsersGroups { get; set; }

		public List<VerificationToken> VerificationTokens { get; set; }
	}

    internal class UserTableConfig 
    {
        public static Action<EntityTypeBuilder<User>> Customize = (user) =>
        {
            user.HasAlternateKey(u => u.Handle);
        };
    }
}