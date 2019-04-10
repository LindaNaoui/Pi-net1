using Domain.Entities;
using PI.Data.Infrastructure;
using Service.Interfaces;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork utwk = new UnitOfWork(databaseFactory);
        public UserService() : base(utwk)
        {

        }

        public IEnumerable<User> noadmin()
        {
            var c = GetMany(t => t.role != "Admin");
            return c;
        }
        public IEnumerable<User> TeamLeader()
        {
            var c = GetMany(t => t.role == "Team Leader");
            return c;
        }
        public IEnumerable<User> TeamMember()
        {
            var c = GetMany(t => t.role == "Team Member");
            return c;
        }
    }

}

