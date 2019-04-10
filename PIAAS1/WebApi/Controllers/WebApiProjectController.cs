using Domain.Entities;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class WebApiProjectController : ApiController
    {
        IProjectServices PS = new ProjectServices();
        // GET: api/WebApiProject
        public IQueryable<Project> GetProject()
        {
            return PS.GetAll().AsQueryable();
        }
        [ResponseType(typeof(Project))]
        // GET: api/WebApiProject/5
        public IHttpActionResult GetOneProject(int id)
        {
            Project P = PS.GetById(id);
            if(P == null)
            {
                return NotFound();
            }
            
            return Ok(P);
        }

        // POST: api/WebApiProject
        public IHttpActionResult Post([FromBody]ProjectApiViewModel PVM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            Project P = new Project();
            P.ProjectId = PVM.ProjectId;
            P.ProjectName = PVM.ProjectName;
            P.Description = PVM.Description;
            P.etat = Domain.Entities.Etat.Active;
            P.Duration = (PVM.End_Date - PVM.Start_Date).TotalDays.ToString();
            P.Start_Date = PVM.Start_Date;
            P.End_Date = PVM.End_Date;
            PS.Add(P);
            PS.Commit();
            return Ok(P);
        }

        // PUT: api/WebApiProject/5
        public IHttpActionResult Put(int id, [FromBody]ProjectApiViewModel PVM)
        {
            Project P = PS.GetById(id);
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            P.ProjectName = PVM.ProjectName;
            P.Description = PVM.Description;
            P.etat = Domain.Entities.Etat.Active;
            P.Duration = (PVM.End_Date - PVM.Start_Date).TotalDays.ToString();
            P.Start_Date = PVM.Start_Date;
            P.End_Date = PVM.End_Date;
            PS.Update(P);
            PS.Commit();
            return Ok(P);
        }

        // DELETE: api/WebApiProject/5
        public IHttpActionResult Delete(int id)
        {
            Project P = PS.GetById(id);
            PS.Delete(P);
            PS.Commit();
            return Ok();

        }

    }
}
