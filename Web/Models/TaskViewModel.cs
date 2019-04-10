using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public enum status { Doing, Done, Todo }
    public class TaskViewModel
    {
        [Key]
        public int TasksId { get; set; }
        public string TaskName { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Duration { get; set; }
        public string Estimation { get; set; }
        public string Description { get; set; }
        public status Status { get; set; }
        public Project Project { get; set; }
        public int ProjectFK { get; set; }
    }
}