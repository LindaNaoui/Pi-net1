using Domain.Entities;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ProjectController : Controller
    {
        IProjectServices ps = new ProjectServices();
        IUserService us = new UserService();
        TaskServices TS = new TaskServices();

        // GET: Project
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User u = us.GetById(myInt);
            var getAll = ps.GetAll().Where(e => e.TeamFK == u.TeamFK);

            return View(getAll);
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            ProjectViewModel PVM = new ProjectViewModel();
            var pip = ps.GetById(id);
            PVM.ProjectId = pip.ProjectId;
            PVM.ProjectName = pip.ProjectName;
            PVM.Start_Date = pip.Start_Date;
            PVM.End_Date = pip.End_Date;
            PVM.Duration = pip.Duration;
            PVM.etat = (Web.Models.Etat)pip.etat;
            //---------------------------------------
            PVM.Description = pip.Description;
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User teamlead = us.GetById(myInt);
            //---------------------------------------
            TeamService teamservice = new TeamService();
            Team t = teamservice.GetById(teamlead.TeamFK);
            //--------------------------------------------
            float progress = TS.ProjectProgress(id);

            var T = TS.GetAll().Where(e => e.ProjectFK == id);
            Console.WriteLine(T);
            List<Tasks> tasks = T.ToList();
            ViewData["Tasks"] = tasks;
            ViewData["TeamLead"] = teamlead;
            ViewData["Team"] = t;
            ViewData["Progress"] = progress;


            return View(PVM);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectViewModel PVM)
        {
            Project P = new Project();
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User u = us.GetById(myInt);

            if (PVM.Start_Date >= DateTime.Now)
            {




                P.ProjectId = PVM.ProjectId;
                P.ProjectName = PVM.ProjectName;
                P.Description = PVM.Description;
                P.etat = Domain.Entities.Etat.Active;
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
            var pip = ps.GetById(id);
            ProjectViewModel PVM = new ProjectViewModel();
            PVM.ProjectName = pip.ProjectName;
            PVM.Start_Date = pip.Start_Date;
            PVM.End_Date = pip.End_Date;
            PVM.Duration = pip.Duration;
            List<string> Etat = new List<string> { "Pending", "Closed", "Active" };
            ViewData["Etat"] = new SelectList(Etat);

            PVM.Description = pip.Description;
            return View(PVM);

        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectViewModel PVM)
        {
            Project P = ps.GetById(id);
            P.ProjectName = PVM.ProjectName;
            P.Start_Date = PVM.Start_Date;
            P.End_Date = PVM.End_Date;
            //P.Duration = (PVM.End_Date - PVM.Start_Date).TotalDays.ToString();
            P.Description = PVM.Description;
            P.etat = (Domain.Entities.Etat)PVM.etat;
            ps.Update(P);
            ps.Commit();
            return RedirectToAction("Index");
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProjectViewModel PVM)
        {
            Project p = ps.GetById(id);

            PVM.ProjectId = p.ProjectId;
            PVM.ProjectName = p.ProjectName;
            PVM.Start_Date = p.Start_Date;
            PVM.End_Date = p.End_Date;
            PVM.Duration = p.Duration;
            PVM.Description = p.Description;
            ps.Delete(p);
            ps.Commit();



            return RedirectToAction("Index");

        }
    }
}
