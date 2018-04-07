using CarCommercial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarCommercial.Controllers
{
    public class OglasivackaAgencijasController : Controller
    {
        private CarCommercialContext db = new CarCommercialContext();

        // GET: OglasivackaAgencijas
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                return View(db.OglasivackaAgencijas.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(OglasivackaAgencija oglasivac)
        {
            if (ModelState.IsValid)
            {
                if (ProvjeraKorisnickogImena(oglasivac.KorisnickoIme) == true)
                {
                    db.OglasivackaAgencijas.Add(oglasivac);
                    db.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Korisničko ime je zauzeto!");
                    return View();
                }
            }
            var korisnik = db.OglasivackaAgencijas.Where(o => o.KorisnickoIme == oglasivac.KorisnickoIme && o.Lozinka == oglasivac.Lozinka).FirstOrDefault();
            Session["ID"] = korisnik.OglasivackaAgencijaID.ToString();
            Session["KorisnickoIme"] = korisnik.KorisnickoIme.ToString();
            Session["Email"] = korisnik.Email.ToString();
            Session["Status"] = "agencija";
            return RedirectToAction("LoggedIn");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(OglasivackaAgencija oglasivac)
        {
            var korisnik = db.OglasivackaAgencijas.Where(o => o.KorisnickoIme == oglasivac.KorisnickoIme && o.Lozinka == oglasivac.Lozinka).FirstOrDefault();
            if (korisnik != null)
            {
                Session["ID"] = korisnik.OglasivackaAgencijaID.ToString();
                Session["KorisnickoIme"] = korisnik.KorisnickoIme.ToString();
                Session["Email"] = korisnik.Email.ToString();
                Session["Status"] = "agencija";
                FormsAuthentication.SetAuthCookie(oglasivac.KorisnickoIme, false);
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
                if (Session["Status"].ToString() != "agencija")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    int id = Convert.ToInt32(Session["ID"].ToString());
                    OglasivackaAgencija agencija = db.OglasivackaAgencijas.Find(id);
                    return View(agencija);
                }
            }
        }

        public bool ProvjeraKorisnickogImena(string username)
        {
            bool provjera = true;
            foreach (var item in db.OglasivackaAgencijas)
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