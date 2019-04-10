using Data;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace Web.Controllers
{
    public class AccountApiController : ApiController
    {
        UserService us = new UserService();



        public AccountApiController()
        {
        }
        // GET: api/AccountApi
        public IQueryable<User> Get()
        {
            return us.GetAll().AsQueryable();
        }

        // GET: api/AccountApi/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUsers(int id)
        {

            User u = us.GetById(id);
            if (u == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(u);
            }
        }

        // POST: api/AccountApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AccountApi/5
        public IHttpActionResult Put(int id, [FromBody]UsersApiModel rvm)
        {
            User user = us.GetById(id);
            if (user == null)
            {
                return NotFound();
            }else
            {
                user.PhoneNumber = rvm.PhoneNumber;
                user.firstname = rvm.firstname;
                user.Email = rvm.Email;
                user.UserName = rvm.Email;
                user.lastname = rvm.lastname;
                user.PasswordHash = rvm.Password;
                us.Update(user);
                us.Commit();
                return Ok(user);
            }

        }

        // DELETE: api/AccountApi/5
        [ResponseType(typeof(User))]
        public IHttpActionResult Delete(int id)
        {
            User user = us.GetById(id);

           if (user == null)
            {
                return NotFound();
            }else
            {
                us.Delete(user);
                us.Commit();
                return Ok(user);

            }
        }

        //[AllowAnonymous]
        //[Route("Register")]
        //public async Task<IHttpActionResult> Register(RegisterViewModel userModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await _repo.RegisterUser(userModel);

        //    IHttpActionResult errorResult = GetErrorResult(result);

        //    if (errorResult != null)
        //    {
        //        return errorResult;
        //    }

        //    return Ok();
        //}

      

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

    }
}
