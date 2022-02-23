using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// DTO za korisnika sistema
    /// </summary>
    public class KorisnikSistemaDto
    {
        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        ///  Korisnicko ime
        /// </summary>
        public string KorisnickoIme { get; set; }

        /// <summary>
        ///  Lozinka
        /// </summary>
        public string Lozinka { get; set; }
    }
}
