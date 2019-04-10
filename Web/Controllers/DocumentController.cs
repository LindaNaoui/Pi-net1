using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Service.Interfaces;
using Service.Services;
using Web.Models;




namespace Web.Controllers
{
    public class DocumentController : Controller
    {

        IDocumentService DS = new DocumentServices();
        // GET: Document
        public ActionResult Index()
        {
            var getAll = DS.GetAll().Where(e => e.categorie == Domain.Entities.Categorie.Image);

            return View(getAll);
        }

        public ActionResult IndexDocument()
        {
            var getAll = DS.GetAll().Where(e => e.categorie == Domain.Entities.Categorie.Document);

            return View(getAll);
        }
        // GET: Document/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Document/Create
        public ActionResult Create()
        {

            List<string> Categorie = new List<string> { "Document", "Image" };
            ViewData["Categorie"] = new SelectList(Categorie);
            return View();
        }

        // POST: Document/Create
        [HttpPost]
        public ActionResult Create(DocumentViewModel DVM, System.Web.HttpPostedFileBase file)
        {
            Document D = new Document();
            D.DocumentId = DVM.DocumentId;
            D.DocumentName = DVM.DocumentName;
            D.Size = DVM.Size;

            D.categorie = (Domain.Entities.Categorie)DVM.categorie;
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Documents/"), fileName);

                    file.SaveAs(path);
                    D.Path = "~/Content/Documents/" + fileName;
                    ViewBag.Message = "File Uploaded Successfully!!";
                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
            D.DateCreation = DateTime.Now;

            DS.Add(D);
            DS.Commit();
            if (DVM.categorie == Web.Models.Categorie.Image)
            {

                return View("Index");
            }

            else
            {
                return View("IndexDocument");
            }
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
