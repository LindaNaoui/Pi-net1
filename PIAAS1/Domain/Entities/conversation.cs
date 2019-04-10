using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class conversation
    {
        public conversation()
        {
            this.sentmessages = new List<sentmessage>();
            this.recievedmessages = new List<recievedmessage>();
        }

        public int id { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public virtual ICollection<sentmessage> sentmessages { get; set; }
        public virtual ICollection<recievedmessage> recievedmessages { get; set; }
    }
}
