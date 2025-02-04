using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Db.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }

        public ICollection<UserGroup> UsersGroups {get; set;}
    }

    internal class GroupTableConfig
    {
        public static Action<EntityTypeBuilder<Group>> Customize = (group) =>
        {
            group.Property(g => g.CreatedDate).HasDefaultValueSql(SqlValue.UtcNow);            
        };
    }
}