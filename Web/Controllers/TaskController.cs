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
    public class TaskController : Controller
    {

        TaskServices TS = new TaskServices();


        // GET: Task
        public ActionResult Index()
        {
            var getAll = TS.GetAll();

            return View(getAll);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
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

        // GET: Task/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(TaskViewModel TVM, int idProject)
        {
            Tasks T = new Tasks();

            T.TasksId = TVM.TasksId;
            T.TaskName = TVM.TaskName;
            T.Start_Date = TVM.Start_Date;
            T.End_Date = TVM.End_Date;
            T.Estimation = TVM.Estimation;
            T.Status = Domain.Entities.status.Done;
            T.Description = TVM.Description;
            T.Duration = TVM.Duration;
            T.ProjectFK = idProject;

            TS.Add(T);
            TS.Commit();
            return RedirectToAction("Details", "Project", new { id = idProject });
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
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

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View();

        }

        // POST: Task/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, TaskViewModel TVM)
        {
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



            return RedirectToAction("Index");
        }
    }
}
