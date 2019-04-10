using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ClientApiViewModel
    {
        [Key]
        public int ClientID { get; set; }


        public string Nom { get; set; }
        public string Prenom { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required]
        public string Email { get; set; }
        public string NumeroTel { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}