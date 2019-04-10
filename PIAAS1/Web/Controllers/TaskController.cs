using Domain.Entities;
using Microsoft.AspNet.Identity;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class TaskController : Controller
    {
        TeamService teamservice = new TeamService();
        TaskServices TS = new TaskServices();
        ProjectServices PS = new ProjectServices();
        IUserService US = new UserService();


        // GET: Task
        public ActionResult Index(int id)
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
            User teammember = US.GetById(myInt);
            var getAll = TS.GetTeamMemberTasks(teammember.Id, id);

            return View(getAll);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var pip = TS.GetById(id);
            TaskViewModel TVM = new TaskViewModel();
            TVM.TasksId = pip.TasksId;
            TVM.TaskName = pip.TaskName;
            TVM.Start_Date = pip.Start_Date;
            TVM.End_Date = pip.End_Date;
            TVM.Duration = pip.Duration;
            TVM.Description = pip.Description;
            TVM.Estimation = pip.Estimation;
            TVM.Status = (Web.Models.status)pip.Status;
            TVM.ProjectFK = pip.ProjectFK;
            TVM.TeamMemberFK = pip.TeamMemberFK;
            TVM.TeamMember = pip.TeamMember;
            //--------------------------------------------
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);
            User teamlead = US.GetById(myInt);
            var tm = US.GetTeamMemberOfTeam(teamlead.TeamFK);
            List<User> teammember = tm.ToList();

           Team t = teamservice.GetById(teamlead.TeamFK);
            ViewData["TeamLead"] = teamlead;

            ViewData["Team"] = t;
            ViewData["TeamMembers"] = teammember;

            return View(TVM);
        }

        // GET: Task/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(TaskViewModel TVM, int idProject)
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
            Tasks T = new Tasks();
            Project P = PS.GetById(idProject);
            if (TVM.Start_Date >= P.Start_Date && TVM.End_Date <= P.End_Date && TVM.Start_Date >= DateTime.Now.Date)
            {
                T.TasksId = TVM.TasksId;
                T.TaskName = TVM.TaskName;
                T.Start_Date = TVM.Start_Date;
                T.End_Date = TVM.End_Date;
                T.Duration = (TVM.End_Date - TVM.Start_Date).TotalDays.ToString();
                T.Status = Domain.Entities.status.Not;
                T.Description = TVM.Description;
                T.ProjectFK = idProject;

                TS.Add(T);
                TS.Commit();
                return RedirectToAction("Details", "Project", new { id = idProject });
            }
            else
            {
                ViewBag.Message = "Tasks dates must be between " + P.Start_Date.ToString("dd/MM/yyyy") + " and " + P.End_Date.ToString("dd/MM/yyyy")+" and Start Date greater tha Today !!!";
                return View("Create");
            }
        }

        // GET: Task/Edit/5
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
            var pip = TS.GetById(id);
            TaskViewModel TVM = new TaskViewModel();
            TVM.TaskName = pip.TaskName;
            TVM.Start_Date = pip.Start_Date;
            TVM.End_Date = pip.End_Date;
            TVM.Duration = pip.Duration;
            TVM.Description = pip.Description;
            TVM.Estimation = pip.Estimation;



            return View(TVM);
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TaskViewModel TVM)
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
            Tasks T = TS.GetById(id);
            T.TaskName = TVM.TaskName;
            T.Start_Date = TVM.Start_Date;
            T.End_Date = TVM.End_Date;
            T.Duration = TVM.Duration;
            T.Description = TVM.Description;
            TVM.Estimation = TVM.Estimation;




            TS.Update(T);
            TS.Commit();
            return RedirectToAction("Index");
        }

        // POST: Task/Delete/5

        public ActionResult Delete(int id, TaskViewModel TVM)
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
            Tasks T = TS.GetById(id);

            TVM.TasksId = T.TasksId;
            TVM.TaskName = T.TaskName;
            TVM.Start_Date = T.Start_Date;
            TVM.End_Date = T.End_Date;
            TVM.Duration = T.Duration;
            TVM.Description = T.Description;
            TVM.Estimation = T.Estimation;

            TS.Delete(T);
            TS.Commit();



            return RedirectToAction("Details", "Project", new { id = T.ProjectFK });
        }
        public ActionResult AffectTask(int idTeamMember, int idTask)
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
            Tasks T = TS.GetById(idTask);
            T.TeamMemberFK = idTeamMember;
            T.Status = Domain.Entities.status.Todo;
            TS.Update(T);
            TS.Commit();

            return RedirectToAction("Details", "Task", new { id = idTask });
        }
    }
}
