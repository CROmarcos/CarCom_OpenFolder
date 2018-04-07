using CarCommercial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarCommercial.Controllers
{
    public class VlasnikAutasController : Controller
    {
        private CarCommercialContext db = new CarCommercialContext();

        // GET: VlasnikAutas
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                return View(db.VlasnikAutas.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(VlasnikAuta vlasnik)
        {
            Session.Clear();
            if (ModelState.IsValid)
            {
                if (ProvjeraKorisnickogImena(vlasnik.KorisnickoIme) == true)
                {
                    db.VlasnikAutas.Add(vlasnik);
                    db.SaveChanges();
                    ModelState.Clear();                    
                }
                else
                {
                    ModelState.AddModelError("", "Korisničko ime je zauzeto!");
                    return View();
                }
            }            
            var korisnik = db.VlasnikAutas.Where(v => v.KorisnickoIme == vlasnik.KorisnickoIme && v.Lozinka == vlasnik.Lozinka).FirstOrDefault();
            Session["ID"] = korisnik.VlasnikAutaID.ToString();
            Session["KorisnickoIme"] = korisnik.KorisnickoIme.ToString();
            Session["Email"] = korisnik.Email.ToString();
            Session["Status"] = "vlasnik";
            return RedirectToAction("LoggedIn");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VlasnikAuta vlasnik)
        {
            Session.Clear();
            var korisnik = db.VlasnikAutas.Where(v => v.KorisnickoIme == vlasnik.KorisnickoIme && v.Lozinka == vlasnik.Lozinka).FirstOrDefault();
            if (korisnik != null)
            {
                Session["ID"] = korisnik.VlasnikAutaID.ToString();
                Session["KorisnickoIme"] = korisnik.KorisnickoIme.ToString();
                Session["Email"] = korisnik.Email.ToString();
                Session["Status"] = "vlasnik";
                FormsAuthentication.SetAuthCookie(vlasnik.KorisnickoIme, false);
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Korisničko ime ili lozinka nisu ispravni!");
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["Status"].ToString() != "vlasnik")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    int id = Convert.ToInt32(Session["ID"].ToString());
                    VlasnikAuta vlasnik = db.VlasnikAutas.Find(id);
                    return View(vlasnik);
                }
            }
        }

        public bool ProvjeraKorisnickogImena(string username)
        {
            bool provjera = true;
            foreach (var item in db.VlasnikAutas)
            {
                if (username == item.KorisnickoIme)
                {
                    provjera = false;
                }
            }
            return provjera;
        }
    }
}