using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Data.Configurations
{
    public class TaskConfiguration : EntityTypeConfiguration<Tasks>
    {
        public TaskConfiguration()
        {
            HasRequired<Project>(e => e.Project).WithMany(e => e.Tasks).HasForeignKey(e => e.ProjectFK);
        }
    }
}
