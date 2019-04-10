using Domain.Entities;
using Service.Services;
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

       ClientServices As = new ClientServices();

        // GET: Client
        public ActionResult Index()
        {
            List<ClientViewModel> list = new List<ClientViewModel>();
            var a = As.GetAll();
            foreach (var i in a)
            {
                ClientViewModel Avm = new ClientViewModel();
                Avm.ClientID = i.Clientid;
                Avm.NomComplet = new NomCompletViewModel() { nom = i.NomComplet.Nom, prenom = i.NomComplet.Prenom };
                list.Add(Avm);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(string recherche)
        {
            List<ClientViewModel> list = new List<ClientViewModel>();
            var a = As.GetAll();
            foreach (var i in a)
            {
                ClientViewModel Avm = new ClientViewModel();
                Avm.ClientID = i.Clientid;
                Avm.NomComplet = new NomCompletViewModel() { nom = i.NomComplet.Nom, prenom = i.NomComplet.Prenom };
                list.Add(Avm);
            }

            if (!String.IsNullOrEmpty(recherche))
            {
                list = list.Where(m => m.NomComplet.nom.Contains(recherche)).ToList();
            }

            return View(list);


        }
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
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
            var e = As.GetById(id);
            ClientViewModel a = new ClientViewModel();
            a.ClientID = e.Clientid;
            a.Email = e.Email;
            a.NumeroTel = e.PhoneNumber;
            a.NomComplet = new NomCompletViewModel() { nom = e.NomComplet.Nom, prenom = e.NomComplet.Prenom };
            return View(a);
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
            Client a = new Client();
            a.Clientid = Avm.ClientID;
            a.Email = Avm.Email;
            a.PhoneNumber = Avm.NumeroTel;
            a.NomComplet = new NomComplet() { Nom = Avm.NomComplet.nom, Prenom = Avm.NomComplet.prenom };
            As.Add(a);
            As.Commit();
            return RedirectToAction("Index");

        }
        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            var pip = As.GetById(id);
            ClientViewModel PVM = new ClientViewModel();
            PVM.NomComplet = new NomCompletViewModel() { nom = pip.NomComplet.Nom, prenom = pip.NomComplet.Prenom };
            PVM.NumeroTel = pip.PhoneNumber;
            PVM.Email = pip.Email;
         
            return View(PVM);
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClientViewModel Avm)
        {
            Client C = As.GetById(id);
            C.Email =Avm.Email;
            C.PhoneNumber = Avm.NumeroTel;
            C.NomComplet.Nom = Avm.NomComplet.nom;
            C.NomComplet.Prenom = Avm.NomComplet.prenom;
            As.Update(C);
            As.Commit();
            return RedirectToAction("Index");
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ClientViewModel Avm)
        {
            Client C = As.GetById(id);
            Avm.ClientID = C.Clientid;
            Avm.Email = C.Email;
            Avm.NumeroTel = C.PhoneNumber;
            Avm.NomComplet = new NomCompletViewModel() { nom = C.NomComplet.Nom, prenom = C.NomComplet.Prenom };
            As.Delete(C);
            As.Commit();
            return RedirectToAction("Index");
        }
    }
}
