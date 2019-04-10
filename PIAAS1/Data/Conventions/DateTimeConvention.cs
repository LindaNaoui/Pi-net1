using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Conventions
{
   public class DateTimeConvention : Convention
    {

        public DateTimeConvention()
        {
            this.Properties<DateTime>().Configure(e => e.HasColumnType("datetime2"));

        }
    }
}
