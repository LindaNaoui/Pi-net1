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
   public  class ClientServices : Service<Client>, IClientServices
    {
        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork unit = new UnitOfWork(databaseFactory);
        public ClientServices() : base (unit)
        {

        }

    }
    
}
