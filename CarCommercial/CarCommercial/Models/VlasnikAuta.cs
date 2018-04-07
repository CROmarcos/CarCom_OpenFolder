using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCommercial.Models
{
    public class VlasnikAuta : Korisnik
    {
        [Display(Name = "# vlasnika auta")]
        public int VlasnikAutaID { get; set; }

        [Required(ErrorMessage = "Potrebno je unijeti ime!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Potrebno je unijeti prezime!")]
        public string Prezime { get; set; }
    }
}