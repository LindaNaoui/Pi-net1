using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public enum Etat { Pending, Active, Closed }
    public class ProjectViewModel
    {

        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime Start_Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime End_Date { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public Etat etat { get; set; }
        public virtual ICollection<Phase> Phases { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public Team Team { get; set; }
        public Client C { get; set; }
        public User TeamLeader { get; set; }
        public int UserFk { get; set; }
        public virtual ICollection<User> TemMembers { get; set; }

    }
}