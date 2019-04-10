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
        public double ProjectProgress(int idproject)
        {
            double all = GetAll().Where(e => e.ProjectFK == idproject).Count();

            double completed = (from i in GetAll()
                                where i.ProjectFK == idproject && i.Status.Equals(Domain.Entities.status.Done)
                                select i).Count();
            double result = completed / all;

            if (all == 0)
            {
                return 0;
            }
            else
                return result * 100.0;
        }

        public double TaskProgression(Tasks task)
        {
            var dayleft = (task.End_Date - DateTime.Now).TotalDays;
            int totaldays = int.Parse(task.Duration);

            return dayleft / totaldays;
        }

        public IEnumerable<Tasks> GetTeamMemberTasks(int idUser, int idProject)
        {
            var tasks = (from i in GetAll()
                         where i.ProjectFK == idProject && i.TeamMemberFK == idUser
                         select i);

            return tasks;
        }

    }
}
