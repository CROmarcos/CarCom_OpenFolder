using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCommercial.Models
{
    public class Rezervacija
    {
        [Display(Name = "Broj rezervacije")]
        public int RezervacijaID { get; set; }

        [Required(ErrorMessage = "Odaberite automobil")]
        [Display(Name = "# automobila")]
        public int AutomobilID { get; set; }

        [Display(Name = "Automobil")]
        public string NazivAuta { get; set; }

        [Required(ErrorMessage = "Odaberite oglas")]
        [Display(Name = "# oglasa")]
        public int OglasID { get; set; }

        [Display(Name = "Oglas")]
        public string NaslovOglasa { get; set; }

        [Required(ErrorMessage = "Unesite broj dana")]
        [Display(Name = "Broj dana")]
        public int BrojDana { get; set; }

        [Display(Name = "Vlasnik automobila")]
        public string Vlasnik { get; set; }

        [Display(Name = "Agencija")]
        public string Agencija { get; set; }

        [Display(Name = "Poklopac motora")]
        public bool PoklopacMotora { get; set; }

        public bool Vrata { get; set; }

        [Display(Name = "Stražnje vjetrobransko staklo")]
        public bool ZadnjeStaklo { get; set; }

        public bool Krov { get; set; }

        public string Datum { get; set; }

        public string Iznos { get; set; }

        [Display(Name = "Zahtjevi oglasa")]
        public string Zahtjevi { get; set; }

        public string IzracunajIznos(int iznos)
        {
            int ukupno = iznos * BrojDana;
            return ukupno.ToString() + ",00 kn";
        }
    }
}
