using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// DTO za korisnika sistema
    /// </summary>
    public class KorisnikSistemaDto
    {
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
