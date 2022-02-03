using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Models
{
    /// <summary>
    /// Model za kreiranje licnosti
    /// </summary>
    public class LicnostCreateDto
    {
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
