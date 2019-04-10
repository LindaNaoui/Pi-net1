using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<User> TeamMembers { get; set; }
        public User TeamLeader { get; set; }

        public int TeamLeaderFK { get; set; }
    }
}
