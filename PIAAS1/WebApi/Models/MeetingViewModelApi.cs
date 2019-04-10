using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class MeetingViewModelApi
    {
        [Key]
        [Display(Name = "ID")]

        public int IdMeet { get; set; }

        [Display(Name = "Description")]

        public string text { get; set; }

        [Display(Name = "Start Date")]

        public DateTime start_date { get; set; }

        [Display(Name = "End Date")]

        public DateTime end_date { get; set; }
        public User user { get; set; }

        public int userFK { get; set; }
    }
}