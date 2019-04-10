using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Domain.Entities;
using Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Microsoft.AspNet.Identity;
namespace Web.Controllers
{
    public class MeetingsController : Controller
    {


        MeetingService ms = new MeetingService();
        UserService us = new UserService();
        // GET: Meeting
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);
            User u = us.GetById(myInt);

            var getAll = ms.GetAll();
            return View(getAll);
        }


        [HttpPost]
        public ActionResult Index(string search)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Meeting> List = new List<Meeting>();

            foreach (var i in ms.GetAll())
            {
                Meeting m = new Meeting();

                m.IdMeet = i.IdMeet;
                m.text = i.text;
                m.start_date = i.start_date;
                m.end_date = i.end_date;

                List.Add(m);
            }
            if (!String.IsNullOrEmpty(search))
            {
                List = List.Where(m => m.text.Contains(search)).ToList();
            }

            return View(List);


        }


        // GET: Meeting/Details/5
        public ActionResult Details(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var d = ms.GetById(id);
            MeetingViewModel mvm = new MeetingViewModel();
            mvm.text = d.text;
            mvm.start_date = d.start_date;
            mvm.end_date = d.end_date;

            return View(mvm);
        }

        // GET: Meeting/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Meeting/Create
        [HttpPost]
        public ActionResult Create(MeetingViewModel mvm)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);
            User u = us.GetById(myInt);

            Meeting m = new Meeting();
            m.IdMeet = mvm.IdMeet;
            m.text = mvm.text;
            m.start_date = mvm.start_date;
            m.end_date = mvm.end_date;

            ms.Add(m);
            ms.Commit();
            return RedirectToAction("Index");

        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var d = ms.GetById(id);
            MeetingViewModel mvm = new MeetingViewModel();
            mvm.text = d.text;
            mvm.start_date = d.start_date;
            mvm.end_date = d.end_date;

            return View(mvm);

        }

        // POST: Meeting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MeetingViewModel mvm)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Meeting m = ms.GetById(id);
            // m.IdMeet = mvm.IdMeet;
            m.text = mvm.text;
            m.start_date = mvm.start_date;
            m.end_date = mvm.end_date;

            ms.Update(m);
            ms.Commit();
            return RedirectToAction("Index");
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var d = ms.GetById(id);
            MeetingViewModel mvm = new MeetingViewModel();

            mvm.text = d.text;
            mvm.start_date = d.start_date;
            mvm.end_date = d.end_date;

            return View(mvm);
        }

        // POST: Meeting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MeetingViewModel mvm)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Meeting m = ms.GetById(id);
            //   m.IdMeet = mvm.IdMeet;
            m.text = mvm.text;
            m.start_date = mvm.start_date;
            m.end_date = mvm.end_date;

            ms.Delete(m);
            ms.Commit();
            return RedirectToAction("Index");
        }



        //UploadDownladFiles

        public ActionResult IndexUpload()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var items = GetFiles();
            return View(items);
        }

        [HttpPost]
        public ActionResult IndexUpload(HttpPostedFileBase file)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File upload successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file";
            }
            var items = GetFiles();
            return View(items);
        }


        public FileResult Download(string FileName)
        {
            var FileVirtualPath = "~/Files/" + FileName;
            return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
        }



        private List<string> GetFiles()
        {

            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Files"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");

            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return items;
        }


        //Calender

        public ActionResult IndexCalender()
        {

            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            return View(sched);
        }

        public ContentResult Data()
        {
            return (new SchedulerAjaxData(
                new MeetingService().GetAll()
                .Select(e => new { e.IdMeet, e.text, e.start_date, e.end_date })
                )
                );
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Meeting>(actionValues);
            var entities = new MeetingService();
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        entities.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.GetAll().FirstOrDefault(ev => ev.IdMeet == action.SourceId);
                        entities.Delete(changedEvent);
                        break;
                    default:// "update"
                        var target = entities.GetAll().Single(e => e.IdMeet == changedEvent.IdMeet);
                        DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                        break;
                }
                entities.Commit();
                action.TargetId = changedEvent.IdMeet;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }
    }
}
