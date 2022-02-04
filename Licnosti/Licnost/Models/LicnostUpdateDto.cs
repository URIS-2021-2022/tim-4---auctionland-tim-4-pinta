using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    /// <summary>
    /// Model za ažuriranje ličnosti
    /// </summary>

    public class LicnostUpdateDto
    {
        /// <summary>
        /// ID ličnosti
        /// </summary>
        public Guid LicnostId { get; set; }

        /// <summary>
        /// Ime ličnosti
        /// </summary>
        public string LicnostIme { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        public string LicnostPrezime { get; set; }

        /// <summary>
        /// Funkcija ličnosti
        /// </summary>
        public string LicnostFunkcija { get; set; }
    }


}
