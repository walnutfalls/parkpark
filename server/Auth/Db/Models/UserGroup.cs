using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Db.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }

    internal class UserGroupTableConfig
    {
        public static Action<EntityTypeBuilder<UserGroup>> Customize = (group) =>
        {
            group.HasKey(ug => new {ug.UserId, ug.GroupId});
        };
    }
}