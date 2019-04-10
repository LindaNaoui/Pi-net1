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
        public NomComplet NomComplet { get; set; }
        public  string Email { get; set; }
        public  string PhoneNumber { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
