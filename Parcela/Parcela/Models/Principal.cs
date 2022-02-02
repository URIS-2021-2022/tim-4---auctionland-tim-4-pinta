using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    /// <summary>
    /// Predstavlja model za autentifikaciju
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisnicko ime
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        public string Password { get; set; }
    }
}
