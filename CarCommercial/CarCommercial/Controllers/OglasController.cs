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
    public class OglasController : Controller
    {
        private CarCommercialContext db = new CarCommercialContext();

        // GET: Oglas
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                return View(db.Oglas.ToList());
            }
        }

        // GET: Oglas/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "agencija")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Oglas oglas = db.Oglas.Find(id);
                    if (oglas == null)
                    {
                        return HttpNotFound();
                    }
                    return View(oglas);
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
                Oglas oglas = db.Oglas.Find(id);
                if (oglas == null)
                {
                    return HttpNotFound();
                }
                return View(oglas);
            }
        }

        // GET: Oglas/Create
        public ActionResult Create()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "agencija")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    return View();
                }
            }
        }

        // POST: Oglas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OglasID,NaslovOglasa,OpisOglasa,Cijena,PoklopacMotora,Vrata,ZadnjeStaklo,Krov,Korisnik")] Oglas oglas)
        {
            oglas.Korisnik = Session["KorisnickoIme"].ToString();
            if (ModelState.IsValid)
            {
                db.Oglas.Add(oglas);
                db.SaveChanges();
                return RedirectToAction("MojiOglasi");
            }

            return View(oglas);
        }

        // GET: Oglas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "agencija")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Oglas oglas = db.Oglas.Find(id);
                    if (oglas == null)
                    {
                        return HttpNotFound();
                    }
                    return View(oglas);
                }
            }
        }

        // POST: Oglas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OglasID,NaslovOglasa,OpisOglasa,Cijena,PoklopacMotora,Vrata,ZadnjeStaklo,Krov,Korisnik")] Oglas oglas)
        {
            oglas.Korisnik = Session["KorisnickoIme"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(oglas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MojiOglasi");
            }
            return View(oglas);
        }

        // GET: Oglas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "agencija")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Oglas oglas = db.Oglas.Find(id);
                    if (oglas == null)
                    {
                        return HttpNotFound();
                    }
                    return View(oglas);
                }
            }
        }

        // POST: Oglas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oglas oglas = db.Oglas.Find(id);
            db.Oglas.Remove(oglas);
            db.SaveChanges();
            return RedirectToAction("MojiOglasi");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MojiOglasi()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            else
            {
                if (Session["Status"].ToString() != "agencija")
                {
                    return RedirectToAction("KrivaUloga", "Home");
                }
                else
                {
                    List<Oglas> lista = new List<Oglas>();
                    foreach (var oglas in db.Oglas)
                    {
                        if (oglas.Korisnik == Session["KorisnickoIme"].ToString())
                        {
                            lista.Add(oglas);
                        }
                    }
                    return View(lista);
                }
            }
        }
    }
}
