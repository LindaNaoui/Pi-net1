using Domain.Entities;
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
    public class MeetingAPIController : ApiController
    {
        
        MeetingService MS = new MeetingService();
        // GET: api/MeetingAPI
        [HttpGet]
        public IQueryable<Meeting> GetMeeting()
        {
            return MS.GetAll().AsQueryable();
        }
        [ResponseType(typeof(Meeting))]

        // GET: api/MeetingAPI/5
        [HttpGet]
        public IHttpActionResult GetOneMeeting(int id)
        {
            Meeting M = MS.GetById(id);
            if (M == null)
            {
                return NotFound();
            }

            return Ok(M);
        }

        // POST: api/MeetingAPI
        [HttpPost]
        public IHttpActionResult Post([FromBody]MeetingViewModelApi MVMA)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            Meeting M = new Meeting();
            M.IdMeet = MVMA.IdMeet;
            M.text = MVMA.text;
            M.start_date = MVMA.start_date;
            M.end_date = MVMA.end_date;
            MS.Add(M);
            MS.Commit();
            return Ok(M);
        }

        // PUT: api/MeetingAPI/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] MeetingViewModelApi MVMA)
        {
            Meeting M = MS.GetById(id);
            M.text = MVMA.text;
            M.start_date = MVMA.start_date;
            M.end_date = MVMA.end_date;
            MS.Update(M);
            MS.Commit();
            return Ok();
        }

        // DELETE: api/MeetingAPI/5
        [HttpDelete]
        public IHttpActionResult Delete(int id, [FromBody] MeetingViewModelApi MVMA)
        {
            Meeting M = MS.GetById(id);
            M.text = MVMA.text;
            M.start_date = MVMA.start_date;
            M.end_date = MVMA.end_date;

            MS.Delete(M);
            MS.Commit();
            return Ok();
        }
    }
}

