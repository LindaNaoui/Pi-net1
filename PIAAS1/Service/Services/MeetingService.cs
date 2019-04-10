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
   public class MeetingService : Service<Meeting>, IMeetingService
    {

        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork unit = new UnitOfWork(databaseFactory);
        public MeetingService() : base(unit)
        {

        }

        public void Remove(Meeting changedEvent)
        {
            throw new NotImplementedException();
        }
    }
}
