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

namespace WebApi.Controllers
{
    public class WebApiDocumentController : ApiController
    {
        IDocumentService DS = new DocumentServices();
        // GET: api/WebApiDocument
        public IQueryable<Document> GetDocuments()
        {
            return DS.GetAll().AsQueryable();

        }

        [ResponseType(typeof(Document))]
        // GET: api/WebApiDocument/5
        public IHttpActionResult GetOneTask(int id)
        {
           Document  D = DS.GetById(id);
            if (D == null)
            {
                return NotFound();
            }

            return Ok(D);
        }

        // POST: api/WebApiDocument
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WebApiDocument/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WebApiDocument/5
        public void Delete(int id)
        {
        }
    }
}
