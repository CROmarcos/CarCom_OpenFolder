using CarCommercial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarCommercial.Controllers
{
    public class HomeController : Controller
    {
        private CarCommercialContext db = new CarCommercialContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult HelpVlasnik()
        {
            return View();
        }

        public ActionResult HelpAgencija()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        //Stranica koja se prikazuje ako korisnik pokuša pristupiti prikazima iz baze prije registracije
        public ActionResult ErrorPage()
        {
            return View();
        }

        //Stranica koja se prikazuje ako vlasnik automobila pokuša unijeti oglas ili obrnuto
        public ActionResult KrivaUloga()
        {
            return View();
        }

        //Prikazuje zauzeta korisnička imena
        public ActionResult PopisKorisnickihImena()
        {
            string vlasnici = "";
            string agencije = "";
            foreach (var item in db.VlasnikAutas)
            {
                vlasnici += item.KorisnickoIme + Environment.NewLine;
            }
            foreach (var item in db.OglasivackaAgencijas)
            {
                agencije += item.KorisnickoIme + Environment.NewLine;
            }
            ViewBag.Vlasnici = vlasnici;
            ViewBag.Agencije = agencije;
            return View();
        }
    }
}