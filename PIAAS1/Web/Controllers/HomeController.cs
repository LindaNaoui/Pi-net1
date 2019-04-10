using Domain.Entities;
using Microsoft.AspNet.Identity;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        UserService us = new UserService();



        public PartialViewResult Userco()
        {

            int currentUserId = 0;
            if (User.Identity.GetUserId() != "")
            {
                currentUserId = Int32.Parse(User.Identity.GetUserId());

            }

            User cuser = us.GetById(currentUserId);
            ViewData["CurrentUser"] = cuser;
            
            return PartialView();




        }
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (User.IsInRole("Team Leader"))
            {
                return RedirectToAction("DisplayTeamLeader", "Home");
            }
            else if (User.IsInRole("Team Member"))
            {
                return RedirectToAction("DisplayTeamMember", "Home");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Users", "Home");
            }

            return View();
        }



        //profil de l'utilisateur
        //Get: /User/id
        public ActionResult Profil(int id )
        {
            var user = us.GetById(id);

            RegisterViewModel rvm = new RegisterViewModel();

            rvm.PhoneNumber = user.PhoneNumber;
            rvm.firstname = user.firstname;
            rvm.Email = user.Email;
            rvm.lastname = user.lastname;
            rvm.Password = user.PasswordHash;
            rvm.Address = user.Address;
            rvm.birthday = user.birthday;


            return View(rvm);
        }



        //liste des utilisateur(admin)
        //get: /Home/Users


        public ActionResult Users()
        {

            //redirect to login if not connected
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            //redirect to nowhere if not admin
            if (!(User.IsInRole("Admin")))
            {
                return RedirectToAction("Nowhere", "Account");


            }


            return View(us.noadmin().ToList());
        }


        //delete User 
        public ActionResult Delete(int id)
        {
            //redirect to login
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            //redirect to nowhere if not admin
            if (!(User.IsInRole("Admin")))
            {
                return RedirectToAction("Nowhere", "Account");


            }
            User user = us.GetById(id);
            us.Delete(user);
            us.Commit();
            return RedirectToAction("Index");
        }


        //Update a User View

        public ActionResult Edit(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            //redirect to nowhere if not admin
            if (!(User.IsInRole("Admin")))
            {
                return RedirectToAction("Nowhere", "Account");


            }

            User user = us.GetById(id);
            RegisterViewModel rvm = new RegisterViewModel();
            rvm.PhoneNumber = user.PhoneNumber;
            rvm.firstname = user.firstname;
            rvm.Email = user.Email;
            rvm.lastname = user.lastname;
            rvm.Password = user.PasswordHash;

            return View(rvm);


        }


        //update User for real 
        [HttpPost]
        public ActionResult Edit(int id, RegisterViewModel rvm)
        {

            User user = us.GetById(id);

            user.PhoneNumber = rvm.PhoneNumber;
            user.firstname = rvm.firstname;
            user.Email = rvm.Email;
            user.UserName = rvm.Email;
            user.lastname = rvm.lastname;
            user.PasswordHash = rvm.Password;

            us.Update(user);
            us.Commit();
            return RedirectToAction("Users");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult DisplayManager()
        {
            ProjectServices ps = new ProjectServices();
            var getAll = ps.GetAll();
            return View(getAll);
        }


        public ActionResult DisplayTeamLeader()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            //redirect to nowhere if not admin
            if (!(User.IsInRole("Team Leader")))
            {
                return RedirectToAction("Nowhere", "Account");
            }
            return View();
        }

        public ActionResult DisplayTeamMember()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            //redirect to nowhere if not admin
            if (!(User.IsInRole("Team Member")))
            {
                return RedirectToAction("Nowhere", "Account");
            }
            return View();
        }
        public ActionResult DisplayAdmin()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            //redirect to nowhere if not admin
            if (!(User.IsInRole("Admin")))
            {
                return RedirectToAction("Nowhere", "Account");
            }
            return View();
        }

    }
}