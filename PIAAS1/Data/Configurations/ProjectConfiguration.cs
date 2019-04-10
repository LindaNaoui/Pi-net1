using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;


namespace Data.Configurations
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {

        public ProjectConfiguration()
        {
            HasRequired<Client>(e => e.C).WithMany(e => e.projets).HasForeignKey(e => e.ClientFK);

            HasRequired<Team>(e => e.Team).WithMany(e => e.Projects).HasForeignKey(e => e.TeamFK);
        }

    }
}
