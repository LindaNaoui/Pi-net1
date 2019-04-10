using Domain.Entities;
using Microsoft.AspNet.Identity;
using Service.Interfaces;
using Service.Services;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ClientController : Controller
    {
        ProjectServices PS = new ProjectServices();

        IUserService us = new UserService();

        ClientServices CS = new ClientServices();

        // GET: Client
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User u = us.GetById(myInt);
            var getAll = CS.GetAll();

            return View(getAll);
        }

        [HttpPost]
        public ActionResult Index(string recherche)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            List<ClientViewModel> list = new List<ClientViewModel>();
            var a = CS.GetAll();
            foreach (var i in a)
            {
                ClientViewModel Avm = new ClientViewModel();
                Avm.ClientID = i.Clientid;
              //  Avm.NomComplet = new NomCompletViewModel() { nom = i.NomComplet.Nom, prenom = i.NomComplet.Prenom };
                list.Add(Avm);
            }

            if (!String.IsNullOrEmpty(recherche))
            {
              //  list = list.Where(m => m.NomComplet.nom.Contains(recherche)).ToList();
            }

            return View(list);


        }

        public ActionResult VoiceExplanation()
        {
            SpVoice spv = new SpVoice();
            spv.Speak("");
            return RedirectToAction("Create");

        }

        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("sweetdays96@gmail.com", "Lilo");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "popovabewild04101996";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ClientViewModel CVM = new ClientViewModel();
            var c = CS.GetById(id);
            CVM.ClientID = c.Clientid;
            CVM.Email = c.Email;
            CVM.NumeroTel = c.PhoneNumber;
            CVM.Nom = c.Nom;
            CVM.Prenom = c.Prenom;
            //-----------------------------------------------------
            var p = PS.GetAll().Where(e => e.ClientFK == 0);
            List<Project> projects = p.ToList();
            ViewData["Projects"] = projects;

            return View(CVM);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(ClientViewModel Avm)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Client a = new Client();
            var currentUserId = User.Identity.GetUserId();
            int myInt = int.Parse(currentUserId);

            User u = us.GetById(myInt);
            a.Clientid = Avm.ClientID;
            a.Email = Avm.Email;
            a.PhoneNumber = Avm.NumeroTel;
            a.Nom = Avm.Nom;
            a.Prenom = Avm.Prenom;
           // a.NomComplet = new NomComplet() { Nom = Avm.NomComplet.nom, Prenom = Avm.NomComplet.prenom };

            CS.Add(a);
            CS.Commit();
            return RedirectToAction("Index");

        }
        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var pip = CS.GetById(id);
            ClientViewModel PVM = new ClientViewModel();
           // PVM.NomComplet = new NomCompletViewModel() { nom = pip.NomComplet.Nom, prenom = pip.NomComplet.Prenom };
            PVM.NumeroTel = pip.PhoneNumber;
            PVM.Nom = pip.Nom;
            PVM.Prenom = pip.Prenom;
            PVM.Email = pip.Email;
         
            return View(PVM);
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClientViewModel Avm)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Client C = CS.GetById(id);
            C.Email =Avm.Email;
            C.PhoneNumber = Avm.NumeroTel;
            C.Nom = Avm.Nom;
            C.Prenom = Avm.Prenom;
            CS.Update(C);
            CS.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Client C = CS.GetById(id);
                       CS.Delete(C);
            CS.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult AffectProjectToClient(int idProject , int idClient)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Project P = PS.GetById(idProject);
            P.ClientFK = idClient;
            PS.Update(P);
            PS.Commit();
            return RedirectToAction("Details", "Client", new { id = idClient });

        }




    }
}
