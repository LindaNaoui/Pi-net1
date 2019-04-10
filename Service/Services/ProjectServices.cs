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
   public class ProjectServices : Service<Project>, IProjectServices
    {
        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork unit = new UnitOfWork(databaseFactory);
        public ProjectServices() : base(unit)
        {

        }
    }
}


