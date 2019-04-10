using Domain.Entities;
using PI.Data.Infrastructure;
using Service.Interfaces;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class TaskServices : Service<Tasks>, ITaskServices
    {
        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork unit = new UnitOfWork(databaseFactory);
        public TaskServices() : base(unit)
        {

        }
        public float ProjectProgress(int idproject)
        {
            var all = GetAll().Where(e => e.ProjectFK == idproject).Count();

            var completed = (from i in GetAll()
                             where i.ProjectFK == idproject && i.Status == Domain.Entities.status.Done
                             select i).Count();
            if (all == 0)
            {
                return 0;
            }
            else
                return (completed / all) * 100;


        }

    }
}
