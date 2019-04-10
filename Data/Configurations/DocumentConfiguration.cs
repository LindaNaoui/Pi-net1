using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Domain.Entities;



namespace Data.Configurations
{
    public class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            HasRequired<Project>(e => e.Project).WithMany(e => e.Documents).HasForeignKey(e => e.ProjectFK);
        }
    }
}
