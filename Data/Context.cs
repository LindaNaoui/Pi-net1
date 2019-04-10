using Data.Configurations;
using Data.Conventions;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.User;

namespace Data
{
   public class Context : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {


        public Context() : base("name=Context")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Conventions.Add(new KeyConvention());
            modelBuilder.Conventions.Add(new DateTimeConvention());
           // modelBuilder.Configurations.Add(new DocumentConfiguration());
          
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Domain.Entities.Tasks>().ToTable("Tasks");
            modelBuilder.Entity<User>().ToTable("User");


        }

        public static Context Create()
        {
            return new Context();
        }
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

    }
}
