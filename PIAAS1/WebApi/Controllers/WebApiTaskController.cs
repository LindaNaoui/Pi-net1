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
    public class WebApiTaskController : ApiController
    {
        ITaskServices TS = new TaskServices();
 
        // GET: api/WebApiTask
        public IQueryable<Tasks> GetTasks()
        {
            return TS.GetAll().AsQueryable();

        }

        [ResponseType(typeof(Tasks))]
        // GET: api/WebApiTask/5
        public IHttpActionResult GetOneTask(int id)
        {
            Tasks T = TS.GetById(id);
            if (T == null)
            {
                return NotFound();
            }

            return Ok(T);
        }

        // POST: api/WebApiTask
        public IHttpActionResult Post([FromBody]TaskApiViewModel TVM, int idProject)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            Tasks T = new Tasks();
            if (T.Project.Start_Date > T.Start_Date && T.Project.End_Date > T.End_Date)
           
                T.TaskName = TVM.TaskName;
                T.Start_Date = TVM.Start_Date;
                T.End_Date = TVM.End_Date;
                T.Status = Domain.Entities.status.Not;
                T.Description = TVM.Description;

                T.Duration = (TVM.End_Date - TVM.Start_Date).TotalDays.ToString();

                TS.Add(T);
                TS.Commit();
                return Ok(T);


           

        }
        // PUT: api/WebApiTask/5
        public IHttpActionResult Put(int id, [FromBody]TaskApiViewModel TVM)
        {
            Tasks T = TS.GetById(id);
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            T.TaskName = TVM.TaskName;
            T.Start_Date = TVM.Start_Date;
            T.End_Date = TVM.End_Date;
            T.Description = TVM.Description;
            T.Estimation = TVM.Estimation;
      
            TS.Update(T);
            TS.Commit();
            return Ok(T);
        }

        // DELETE: api/WebApiTask/5
        public IHttpActionResult Delete(int id)
        {
            Tasks T = TS.GetById(id);
            TS.Delete(T);
            TS.Commit();
            return Ok(T);
        }

    }
}
