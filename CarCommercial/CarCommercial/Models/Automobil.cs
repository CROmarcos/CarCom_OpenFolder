using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCommercial.Models
{
    public class Automobil
    {
        [Display(Name = "# vozila")]
        public int AutomobilID { get; set; }

        [Required(ErrorMessage = "Potrebno je navesti marku vozila.")]
        public string Marka { get; set; }

        [Required(ErrorMessage = "Potrebno je navesti model vozila.")]
        public string Model { get; set; }

        [Display(Name = "Godište")]
        [Required(ErrorMessage = "Molimo, unesite godinu proizvodnje.")]
        public int GodinaProizvodnje { get; set; }

        [Required(ErrorMessage = "Potrebno je unijeti registraciju.")]
        public string Registracija { get; set; }

        public ConsoleColor Boja { get; set; }

        //Varijabla pomoću koje će se dolaziti do korisnika koji je postavio automobil
        public string Korisnik { get; set; }
    }
}