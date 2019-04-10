using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
   public class PhasesConfigurations : EntityTypeConfiguration<Domain.Entities.Phase>
    {
        public PhasesConfigurations()
        {
            HasRequired<Project>(t => t.Project).WithMany(t => t.Phases).HasForeignKey(t => t.ProjectFK).WillCascadeOnDelete(true);

        }
    }
}
