using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class MultiV
    {
        public IEnumerable<Domain.Entities.User> User {get;set; }
        public RegisterViewModel lvm { get; set; }
    }
}