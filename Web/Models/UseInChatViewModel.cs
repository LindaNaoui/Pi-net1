using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace chatfinal.Models
{
    public class UseInChatViewModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string UrlPhoto { get; set; }
        public Nullable<System.DateTime> lastLogin { get; set; }
    }
}