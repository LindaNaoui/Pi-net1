using Domain.Entities;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class WebAPIProjectController : ApiController
    {
        IProjectServices PS = new ProjectServices();
        // GET: api/WebAPIProject
        public List<Project> GetProjects()
        {
            var p = PS.GetAll();
            List<Project> projects = p.ToList();
            return projects;
        }

        // GET: api/WebAPIProject/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WebAPIProject
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WebAPIProject/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WebAPIProject/5
        public void Delete(int id)
        {
        }
    }
}
