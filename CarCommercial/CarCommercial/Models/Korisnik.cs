using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarCommercial.Models
{
    public abstract class Korisnik
    {
        [Display(Name = "E-mail adresa")]
        [Required(ErrorMessage = "Potrebno je unijeti e-mail!")]
        public string Email { get; set; }

        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "Potrebno je unijeti korisničko ime!")]
        public string KorisnickoIme { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Potrebna je lozinka!")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Display(Name = "Potvrda lozinke")]
        [Compare("Lozinka", ErrorMessage = "Molimo, potvrdite lozinku.")]
        [DataType(DataType.Password)]
        public string PotvrdaLozinke { get; set; }

        [Display(Name = "Broj telefona/mobitela")]
        [DataType(DataType.PhoneNumber)]
        public string BrojTelefona { get; set; }
    }
}
