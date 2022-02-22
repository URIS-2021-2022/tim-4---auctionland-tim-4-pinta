using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.Models
{
    /// <summary>
    /// DTO za adresu kupca ugovora
    /// </summary>
    public class AdresaKupcaUgovoraDto
    {
        /// <summary>
        /// Ulica kupca
        /// </summary>
        public string Ulica { get; set; }

        /// <summary>
        /// Broj kupca
        /// </summary>
        public string Broj { get; set; }

        /// <summary>
        /// Mesto kupca
        /// </summary>
        public string Mesto { get; set; }

        /// <summary>
        /// Postanski broj kupca
        /// </summary>
        public string PostanskiBroj { get; set; }
    }
}
