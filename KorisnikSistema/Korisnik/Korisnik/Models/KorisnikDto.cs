using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Models
{
    /// <summary>
    /// DTO za prijavu ispita
    /// </summary>
    public class KorisnikDto
    {
        /// <summary>
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
    }
}
