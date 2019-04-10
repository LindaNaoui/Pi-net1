using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.User;

namespace Domain.Entities
{

    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {


        public string firstname { get; set; }
        public string gender { get; set; }

        public string country { get; set; }

        public string Address { get; set; }
        public DateTime birthday{ get; set; }


        public string lastname { get; set; }

         public string img { get; set; }

        public string role { get; set; }

        public Team Team { get; set; }

        public int TeamFK { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }

        public class CustomRole : IdentityRole<int, CustomUserRole>
        {
            public CustomRole() { }
            public CustomRole(string name) { Name = name; }
        }


        public class CustomUserClaim : IdentityUserClaim<int>
        {

        }


        public class CustomUserLogin : IdentityUserLogin<int>
        {
            public int Id { get; set; }

        }
        public class CustomUserRole : IdentityUserRole<int>
        {
            public int Id { get; set; }
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }

    }
}
