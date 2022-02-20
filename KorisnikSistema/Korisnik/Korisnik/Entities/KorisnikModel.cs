using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Models
{
    /// <summary>
    /// Predstavlja model korisnika
    /// </summary>
    public class KorisnikModel
    {
        /// <summary>
        /// Id korisnika
        /// </summary>
        /// 
        [Key]
        public int KorisnikId { get; set; }
        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string Ime { get; set; }
        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string Prezime { get; set; }
        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string KorisnickoIme { get; set; }
        /// <summary>
        /// Hash-ovana šifra korisnika
        /// </summary>
        public string Lozinka { get; set; }
        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }
    }
}
