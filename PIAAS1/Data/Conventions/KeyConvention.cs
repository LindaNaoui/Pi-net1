using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Conventions
{
   public class KeyConvention : Convention
    {
        public KeyConvention()
        {
            //Properties().Where(t => t.Name.EndsWith("Id")).Configure(t => t.IsKey());

        }
          
    }
}
