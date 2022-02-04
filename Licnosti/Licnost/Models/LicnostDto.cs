using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    /// <summary>
    /// Predstavlja model jedne licnosti
    /// </summary>
    public class LicnostDto
    {
        /// <summary>
        /// Ime i prezime ličnosti
        /// </summary>
        public string LicnostImePrezime { get; set; }
        /// <summary>
        /// Funkcija ličnosti
        /// </summary>
        public string LicnostFunkcija { get; set; }
    }
}
