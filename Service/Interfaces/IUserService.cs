using Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
   public interface IUserService : IService<User>
    {

        IEnumerable<User> noadmin();
    }
}
