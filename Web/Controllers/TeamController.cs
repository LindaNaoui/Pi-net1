using Domain.Entities;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class TeamController : Controller
    {
        TeamService tm = new TeamService();
        UserService us = new UserService();
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            // UserService us = new UserService();
            //List <User> lists = new List<User>();
            //foreach (var item in us.TeamLeader())
            //{
            // User U = new User();
            //  U.firstname = item.firstname;
            //lists.Add(U);
            // }

            //List<User> lists1 = new List<User>();
            //foreach (var item in us.TeamMember())
            // {
            //   User U = new User();
            //  U.firstname = item.firstname;
            //  lists.Add(U);
            // }

            us.TeamLeader().ToList();
            us.TeamMember().ToList();

            return View();


            // return ViewData;

        }

        // POST: Team/Create
        [HttpPost]
       // public ActionResult Create(TeamViewModel TVM)
        
       
         //   Team T = new Team();
            //T.TeamName = TVM.TeamName;


     //   }
      //  }
    
        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
