using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarCommercial.Models;

namespace CarCommercial.Controllers
{
    public class AutomobilsController : Controller
    {
        private CarCommercialContext db = new CarCommercialContext();

        // GET: Automobils
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                return View(db.Automobils.ToList());
            }
        }

        // GET: Automobils/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "vlasnik")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Automobil automobil = db.Automobils.Find(id);
                    if (automobil == null)
                    {
                        return HttpNotFound();
                    }
                    return View(automobil);
                }
            }
        }

        public ActionResult DetailsOpcenito(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Automobil automobil = db.Automobils.Find(id);
                if (automobil == null)
                {
                    return HttpNotFound();
                }
                return View(automobil);
            }
        }

        // GET: Automobils/Create
        public ActionResult Create()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "vlasnik")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    return View();
                }
            }
        }

        // POST: Automobils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AutomobilID,Marka,Model,GodinaProizvodnje,Registracija,Boja,Korisnik")] Automobil automobil)
        {
            automobil.Korisnik = Session["KorisnickoIme"].ToString();
            if (ModelState.IsValid)
            {
                db.Automobils.Add(automobil);
                db.SaveChanges();
                return RedirectToAction("MojiAuti");
            }

            return View(automobil);
        }

        // GET: Automobils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "vlasnik")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Automobil automobil = db.Automobils.Find(id);
                    if (automobil == null)
                    {
                        return HttpNotFound();
                    }
                    return View(automobil);
                }
            }
        }

        // POST: Automobils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AutomobilID,Marka,Model,GodinaProizvodnje,Registracija,Boja,Korisnik")] Automobil automobil)
        {
            automobil.Korisnik = Session["KorisnickoIme"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(automobil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MojiAuti");
            }
            return View(automobil);
        }

        // GET: Automobils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "vlasnik")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Automobil automobil = db.Automobils.Find(id);
                    if (automobil == null)
                    {
                        return HttpNotFound();
                    }
                    return View(automobil);
                }
            }
        }

        // POST: Automobils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Automobil automobil = db.Automobils.Find(id);
            db.Automobils.Remove(automobil);
            db.SaveChanges();
            return RedirectToAction("MojiAuti");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MojiAuti()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "vlasnik")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    List<Automobil> lista = new List<Automobil>();
                    foreach (var auto in db.Automobils)
                    {
                        if (auto.Korisnik == Session["KorisnickoIme"].ToString())
                        {
                            lista.Add(auto);
                        }
                    }
                    return View(lista);
                }
            }
        }
    }
}
