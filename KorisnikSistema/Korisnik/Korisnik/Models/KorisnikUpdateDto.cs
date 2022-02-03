using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Models
{
    public class KorisnikUpdateDto : IValidatableObject
    {
        public int KorisnikId { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }

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
