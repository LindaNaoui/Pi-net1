using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class TeamViewModel
    {

        [Key]
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<User> TeamMembers { get; set; }
        public User TeamLeader { get; set; }
        public int TeamLeaderFK { get; set; }
    }
}