using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Data.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasRequired<Team>(e => e.Team).WithMany(e => e.TeamMembers).HasForeignKey(e => e.TeamFK);

        }
    }
}
