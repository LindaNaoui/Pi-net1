using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
   public class MeetingConfiguration : EntityTypeConfiguration<Meeting>
    {
        public MeetingConfiguration()
        {
            HasRequired<User>(e => e.user).WithMany(e => e.Meetings).HasForeignKey(e => e.userFK);
        }
      
    }
}
