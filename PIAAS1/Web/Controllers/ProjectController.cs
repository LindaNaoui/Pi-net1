using Domain.Entities;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    public class ProjectController : Controller
    {
        ProjectServices ps = new ProjectServices();
        TeamService teamservice = new TeamService();
        IUserService us = new UserService();
        ITaskServices TS = new TaskServices();
        IDocumentService DS = new DocumentServices();

        // GET: Project
        public ActionResult Index()
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
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User u = us.GetById(myInt);
            var getAll = ps.GetAll().Where(e => e.TeamFK == u.TeamFK);

            return View(getAll);
            
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            ProjectViewModel PVM = new ProjectViewModel();
            var pi = ps.GetById(id);
            PVM.ProjectId = pi.ProjectId;
            PVM.ProjectName = pi.ProjectName;
            PVM.Start_Date = pi.Start_Date;
            PVM.End_Date = pi.End_Date;
            PVM.Duration = pi.Duration;
            PVM.etat = (Web.Models.Etat)pi.etat;
            //---------------------------------------
            PVM.Description = pi.Description;
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User teamlead = us.GetById(myInt);
            //---------------------------------------
            Team t = teamservice.GetById(teamlead.TeamFK);
            //--------------------------------------------
            double progress = TS.ProjectProgress(id);

            var T = TS.GetAll().Where(e => e.ProjectFK == id);


            //foreach (var task in T)
            //{
            //    if (task.TeamMemberFK != 0)
            //    {
            //        User u = us.GetById(task.TeamMemberFK);

            //          //    task.TeamMember = u;
            //        //        //task.TeamMember.firstname = u.firstname;
            //        //        //task.TeamMember.lastname = u.lastname;
            //        //        //task.TeamMember.firstname = u.firstname;
            //        //        //task.TeamMember.Email = u.Email;
            //    }
            //}

            List<Tasks> tasks = T.ToList();

            //------------------------------------------------
            var doc = DS.GetAll().Where(e => e.ProjectFK == id);
            List<Document> docs = doc.ToList();

            //--------------------------------------------------
            var tm = us.GetTeamMemberOfTeam(teamlead.TeamFK);
            List<User> teammembers = tm.ToList();

            ViewData["Tasks"] = tasks;
            ViewData["TeamLead"] = teamlead;
            ViewData["Team"] = t;
            ViewData["Progress"] = progress;
            ViewData["Documents"] = docs;
            ViewData["TeamMembers"] = teammembers;


            return View(PVM);
        }

        // GET: Project/Create
        public ActionResult Create()
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

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectViewModel PVM)
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
            Project P = new Project();
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User u = us.GetById(myInt);

            if (PVM.Start_Date >= DateTime.Now.Date)
            {
                P.ProjectId = PVM.ProjectId;
                P.ProjectName = PVM.ProjectName;
                P.Description = PVM.Description;
                P.etat = Domain.Entities.Etat.Pending;
                P.Duration = (PVM.End_Date - PVM.Start_Date).TotalDays.ToString();
                P.Start_Date = PVM.Start_Date;
                P.End_Date = PVM.End_Date;
                P.TeamFK = u.TeamFK;
                ps.Add(P);
                ps.Commit();

                return RedirectToAction("index");
            }
            else
                ViewBag.Message = "Start date must be greater than Today !";
            return View("Create");

        }

        public ActionResult CreateTest(ProjectViewModel PVM)
        {

            return View();

        }
        // GET: Project/Edit/5
        public ActionResult Edit(int id)
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
            var pip = ps.GetById(id);
            ProjectViewModel PVM = new ProjectViewModel();
            PVM.ProjectName = pip.ProjectName;
            PVM.Start_Date = pip.Start_Date;
            PVM.End_Date = pip.End_Date;
            PVM.Duration = pip.Duration;

            PVM.Description = pip.Description;
            return View(PVM);

        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectViewModel PVM)
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
            Project P = ps.GetById(id);



            P.ProjectName = PVM.ProjectName;
            P.Start_Date = PVM.Start_Date;
            P.End_Date = PVM.End_Date;
            //P.Duration = (PVM.End_Date - PVM.Start_Date).TotalDays.ToString();
            P.Description = PVM.Description;

            ps.Update(P);
            ps.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
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
            Project p = ps.GetById(id);


            ps.Delete(p);
            ps.Commit();



            return RedirectToAction("Index");

        }

        public ActionResult MyProjects()
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
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);
            User teammember = us.GetById(myInt);
            var userteam = teammember.TeamFK;

            var tasks = (from j in TS.GetAll() where j.TeamMemberFK == teammember.Id select j.ProjectFK);

            var projects = (from i in ps.GetAll()
                            where i.TeamFK == userteam && tasks.Contains(i.ProjectId)
                            select i);
            ViewData["TeamMemberProjects"] = projects;

            return View(projects);
        }


    }
}
