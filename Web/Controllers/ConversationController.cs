using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Web.Models;
using PI.Domain;

namespace Web.Controllers
{
    public class ConversationController : Controller
    {
        // GET: Conversation
        public ActionResult Index()
        {
           
            return View();
           
        }
        //public ActionResult chat()
        //{
        //    return View("ChatPage.aspx");
        //}
        // GET: Conversation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Conversation/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("create");
        }

        // POST: Conversation/Create
        [HttpPost]
        public ActionResult Create(conversation conv)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2663/");

                client.PostAsJsonAsync<conversation>("api/conversation", conv).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Conversation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Conversation/Edit/5
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

        // GET: Conversation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Conversation/Delete/5
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
