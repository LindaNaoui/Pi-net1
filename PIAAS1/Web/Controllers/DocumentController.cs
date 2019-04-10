using Domain.Entities;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Web.Models;


namespace Web.Controllers
{
    public class DocumentController : Controller
    {
        IDocumentService DS = new DocumentServices();
        IProjectServices PS = new ProjectServices();
        // GET: Document
        public ActionResult Index()
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
            var getAll = DS.GetAll().Where(e => e.categorie == Domain.Entities.Categorie.Image);
            return View(getAll);
        }
        public ActionResult IndexDocument()
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
            var getAll = DS.GetAll().Where(e => e.categorie == Domain.Entities.Categorie.Document);

            return View(getAll);
        }
        // GET: Document/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Document/Create
        public ActionResult Create(int idProject)
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
            ViewData["id"] = idProject;
            return View();
        }

        // POST: Document/Create
        [HttpPost]
        public ActionResult Create(int idProject, HttpPostedFileBase file, DocumentViewModel DVM)
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
            Project P = PS.GetById(idProject);
            Document D = new Document();
            D.DocumentId = DVM.DocumentId;
            D.DocumentName = DVM.DocumentName;
            D.categorie = (Domain.Entities.Categorie)DVM.categorie;
            try
            {
                if (file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Documents/"), fileName);
                    file.SaveAs(path);
                    DVM.Size = file.ContentLength / 1024;
                    D.Path = "~/Content/Documents/" + fileName;
                    D.Size = DVM.Size;
                    D.DateCreation = DateTime.Now;
                    D.ProjectFK = P.ProjectId;
                    ViewBag.Message = "File Uploaded Successfully!!";
                    DS.Add(D);
                    DS.Commit();

                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View(DVM);
            }


            return RedirectToAction("Details", "Project", new {id = idProject });

        }
        // GET: Document/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Document/Edit/5
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

        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(DocumentViewModel DVM)
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
            Document D = new Document();
            D.DocumentId = DVM.DocumentId;
            D.DocumentName = DVM.DocumentName;


            DS.Delete(D);
            DS.Commit();
            return RedirectToAction("Index");
        }



    }
}
