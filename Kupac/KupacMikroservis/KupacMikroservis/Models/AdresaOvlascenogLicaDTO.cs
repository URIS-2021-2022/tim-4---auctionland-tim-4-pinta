using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// DTO za adresu ovlascenog lica
    /// </summary>
    public class AdresaOvlascenogLicaDto
    {
        /// <summary>
        /// Ulica ovlascenog lica
        /// </summary>
        public string Ulica { get; set; }

        /// <summary>
        /// Broj ovlascenog lica
        /// </summary>
        public string Broj { get; set; }

        /// <summary>
        /// Mesto ovlascenog lica
        /// </summary>
        public string Mesto { get; set; }

        /// <summary>
        /// Postanski broj ovlascenog lica
        /// </summary>
        public string PostanskiBroj { get; set; }




    }
}
