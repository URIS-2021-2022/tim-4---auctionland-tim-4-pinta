using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Models
{
    /// <summary>
    /// DTO za adresu kupca
    /// </summary>
    public class AdresaKupcaDTO
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
