using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// DTO ličnosti ugovora
    /// </summary>
    public class LicnostUgovoraDto
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
