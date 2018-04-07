using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCommercial.Models
{
    public class OglasivackaAgencija : Korisnik
    {
        [Display(Name = "# agencije")]
        public int OglasivackaAgencijaID { get; set; }

        [Display(Name = "Naziv tvrtke")]
        [Required(ErrorMessage = "Potrebno je unijeti naziv tvrtke!")]
        public string NazivTvrtke { get; set; }

        [Display(Name = "Sjedište poslovanja tvrtke")]
        [Required(ErrorMessage = "Unesite grad u kojem tvrtka posluje!")]
        public string Sjediste { get; set; }
    }
}