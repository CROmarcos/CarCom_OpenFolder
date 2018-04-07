using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarCommercial.Models;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace CarCommercial.Controllers
{
    public class RezervacijasController : Controller
    {
        private CarCommercialContext db = new CarCommercialContext();

        // GET: Rezervacijas/Create
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
                    NapraviPopis();
                    return View();
                }
            }
        }

        // POST: Rezervacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RezervacijaID,AutomobilID,OglasID,BrojDana,Vlasnik,Agencija,PoklopacMotora,Vrata,ZadnjeStaklo,Krov")] Rezervacija rezervacija)
        {
            Automobil auto = new Automobil();
            Oglas oglas = new Oglas();
            foreach (var item in db.Automobils)
            {
                if (item.AutomobilID == rezervacija.AutomobilID)
                {
                    auto = item;
                }
            }
            foreach (var item in db.Oglas)
            {
                if (item.OglasID == rezervacija.OglasID)
                {
                    oglas = item;
                }
            }
            rezervacija.Vlasnik = auto.Korisnik;
            rezervacija.NazivAuta = auto.Marka + " " + auto.Model;
            rezervacija.Agencija = oglas.Korisnik;
            rezervacija.NaslovOglasa = oglas.NaslovOglasa;
            rezervacija.Datum = DateTime.Now.ToLongDateString();

            rezervacija.Zahtjevi = "";
            int ukupno = 0;
            int brojac = 0;

            if (rezervacija.PoklopacMotora == true)
            {
                ukupno += oglas.PoklopacMotora;
                rezervacija.Zahtjevi += "poklopac motora";
                brojac++;
            }
            if (rezervacija.Vrata == true)
            {
                ukupno += oglas.Vrata;
                if (brojac > 0)
                {
                    rezervacija.Zahtjevi += ", " + Environment.NewLine;
                }
                rezervacija.Zahtjevi += "vrata";
                brojac++;
            }
            if (rezervacija.ZadnjeStaklo == true)
            {
                ukupno += oglas.ZadnjeStaklo;
                if (brojac > 0)
                {
                    rezervacija.Zahtjevi += ", " + Environment.NewLine;
                }
                rezervacija.Zahtjevi += "stražnje staklo";
                brojac++;
            }
            if (rezervacija.Krov == true)
            {
                ukupno += oglas.Krov;
                if (brojac > 0)
                {
                    rezervacija.Zahtjevi += ", " + Environment.NewLine;
                }
                rezervacija.Zahtjevi += "krov";
            }

            rezervacija.Iznos = rezervacija.IzracunajIznos(ukupno);


            if (ModelState.IsValid)
            {
                db.Rezervacijas.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("MojeRezervacijeVlasnik");
            }
            return View(rezervacija);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void NapraviPopis()
        {
            List<SelectListItem> popisAuta = new List<SelectListItem>();
            foreach (var item in db.Automobils)
            {
                if (item.Korisnik == Session["KorisnickoIme"].ToString())
                {
                    popisAuta.Add(new SelectListItem() { Value = item.AutomobilID.ToString(), Text = item.Marka + " " + item.Model });
                }
            }
            ViewBag.PopisAuta = popisAuta;
            List<SelectListItem> popisOglasa = new List<SelectListItem>();
            foreach (var item in db.Oglas)
            {
                popisOglasa.Add(new SelectListItem() { Value = item.OglasID.ToString(), Text = item.NaslovOglasa });
            }
            ViewBag.PopisOglasa = popisOglasa;
        }

        public ActionResult MojeRezervacijeVlasnik()
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
                    List<Rezervacija> lista = new List<Rezervacija>();
                    foreach (var item in db.Rezervacijas)
                    {
                        if (item.Vlasnik == Session["KorisnickoIme"].ToString())
                        {
                            lista.Add(item);
                        }
                    }
                    return View(lista);
                }
            }
        }

        public ActionResult MojeRezervacijeAgencija()
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
                    List<Rezervacija> lista = new List<Rezervacija>();
                    foreach (var item in db.Rezervacijas)
                    {
                        if (item.Agencija == Session["KorisnickoIme"].ToString())
                        {
                            lista.Add(item);
                        }
                    }
                    return View(lista);
                }
            }
        }

        public ActionResult NapraviIzvjestaj(int? id)
        {
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            List<Rezervacija> listaRezervacija = new List<Rezervacija>();
            listaRezervacija.Add(rezervacija);

            Automobil auto = db.Automobils.Find(rezervacija.AutomobilID);
            List<Automobil> listaAuta = new List<Automobil>();
            listaAuta.Add(auto);

            Oglas oglas = db.Oglas.Find(rezervacija.OglasID);
            List<Oglas> listaOglasa = new List<Oglas>();
            listaOglasa.Add(oglas);

            VlasnikAuta vlasnik = new VlasnikAuta();
            foreach (var item in db.VlasnikAutas)
            {
                if (rezervacija.Vlasnik == item.KorisnickoIme)
                {
                    vlasnik = item;
                }
            }
            List<VlasnikAuta> listaVlasnika = new List<VlasnikAuta>();
            listaVlasnika.Add(vlasnik);

            OglasivackaAgencija agencija = new OglasivackaAgencija();
            foreach (var item in db.OglasivackaAgencijas)
            {
                if (rezervacija.Agencija == item.KorisnickoIme)
                {
                    agencija = item;
                }
            }
            List<OglasivackaAgencija> listaAgencija = new List<OglasivackaAgencija>();
            listaAgencija.Add(agencija);

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "RezervacijaIzvjestaj.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction("MojeRezervacijeVlasnik");
            }
            ReportDataSource rd = new ReportDataSource("MojDataSet", listaRezervacija);
            lr.DataSources.Add(rd);
            lr.DataSources.Add(new ReportDataSource("DsAutomobil", listaAuta));
            lr.DataSources.Add(new ReportDataSource("DsOglas", listaOglasa));
            lr.DataSources.Add(new ReportDataSource("DsVlasnik", listaVlasnika));
            lr.DataSources.Add(new ReportDataSource("DsAgencija", listaAgencija));
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }
    }
}

