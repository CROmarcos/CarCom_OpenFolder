using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCommercial.Models
{
    public class Oglas
    {
        [Display(Name = "# oglasa")]
        public int OglasID { get; set; }

        [Display(Name = "Naslov oglasa")]
        [Required(ErrorMessage = "Potrebno je staviti naslov oglasa")]
        public string NaslovOglasa { get; set; }

        [Display(Name = "Opis oglasa")]
        public string OpisOglasa { get; set; }

        [Display(Name = "Poklopac motora")]
        [Required(ErrorMessage = "Cijena obavezna")]
        public int PoklopacMotora { get; set; }

        [Required(ErrorMessage = "Cijena obavezna")]
        public int Vrata { get; set; }

        [Display(Name = "Stražnje vjetrobransko staklo")]
        [Required(ErrorMessage = "Cijena obavezna")]
        public int ZadnjeStaklo { get; set; }

        [Required(ErrorMessage = "Cijena obavezna")]
        public int Krov { get; set; }

        //Varijabla pomoću koje će se dolaziti do korisnika koji je postavio automobil
        public string Korisnik { get; set; }
    }
}