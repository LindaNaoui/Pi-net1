using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ClientViewModel
    {
        [Key]
        public int ClientID { get; set; }
        public  NomCompletViewModel  NomComplet{ get; set; }
        public string  Email { get; set; }
        public string  NumeroTel { get; set; }
    }
}