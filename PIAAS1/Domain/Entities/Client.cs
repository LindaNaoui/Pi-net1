using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public  class Client
    {
        public int Clientid { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public  string PhoneNumber { get; set; }

        public virtual ICollection<Project> projets { get; set; }
        public User user { get; set; }
        public int UserFK { get; set; }
    }
}
