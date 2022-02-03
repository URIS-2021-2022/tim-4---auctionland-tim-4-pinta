using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Models
{/// <summary>
 /// Model za kreiranje prijave ispita
 /// </summary>
    public class KorisnikCreationDto : IValidatableObject
    {  /// <summary>
       /// ID studenta.
       /// </summary>
        public int KorisnikId { get; set; }
        /// <summary>
        /// Ime studenta.
        /// </summary>
        public string Ime { get; set; }
        /// <summary>
        /// Prezime studenta.
        /// </summary>
        public string Prezime { get; set; }
        /// <summary>
        ///  korisnicko ime .
        /// </summary>
        public string KorisnickoIme { get; set; }
        /// <summary>
        ///  lozinka
        /// </summary>
        public string Lozinka { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ime == Prezime)
            {
                yield return new ValidationResult("Korisnik ne moze da ima isto ime i prezime", new[] { "ExamRegistrationCreationDto" });
            }
        }
    }
}
