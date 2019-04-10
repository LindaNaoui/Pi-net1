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
    public class WebApiClientController : ApiController
    {

        IClientServices CS = new ClientServices();
        // GET: api/WebApiClient
        public IQueryable<Client> GetProject()
        {
            return CS.GetAll().AsQueryable();
        }
        [ResponseType(typeof(Project))]
        // GET: api/WebApiClient/5
        public IHttpActionResult GetOneProject(int id)
        {
            Client C = CS.GetById(id);
            if (C == null)
            {
                return NotFound();
            }

            return Ok(C);
        }

        // POST: api/WebApiClient
        public IHttpActionResult Post([FromBody]ClientApiViewModel CVM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            Client C = new Client();
            C.Clientid = CVM.ClientID;
            C.Nom = CVM.Nom;
            C.Prenom = CVM.Prenom;
            C.Email = CVM.Email;
            C.PhoneNumber = CVM.NumeroTel;
            CS.Add(C);
            CS.Commit();
            return Ok(C);
        }

        // PUT: api/WebApiClient/5
        public IHttpActionResult Put(int id, [FromBody]ClientApiViewModel CVM)
        {
            Client C = CS.GetById(id);
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            C.Nom = CVM.Nom;
            C.Prenom = CVM.Prenom;
            C.Email = CVM.Email;
            C.PhoneNumber = CVM.NumeroTel;
            CS.Update(C);
            CS.Commit();
            return Ok(C);
        }

        // DELETE: api/WebApiClient/5
        public IHttpActionResult Delete(int id)
        {
            Client C = CS.GetById(id);
            CS.Delete(C);
            CS.Commit();
            return Ok();

        }

    }
}