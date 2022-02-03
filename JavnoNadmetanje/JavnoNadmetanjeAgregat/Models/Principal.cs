using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// Model za autentifikaciju korisnika
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
