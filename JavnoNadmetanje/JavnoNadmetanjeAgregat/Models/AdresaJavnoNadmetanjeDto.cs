using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Models
{
    /// <summary>
    /// DTO za adresu javnog nadmetanja
    /// </summary>
    public class AdresaJavnoNadmetanjeDto
    {
        /// <summary>
        /// Ulica adrese
        /// </summary>
        public string Ulica { get; set; }
        /// <summary>
        /// Broj adrese
        /// </summary>
        public string Broj { get; set; }
        /// <summary>
        /// Mesto adrese
        /// </summary>
        public string Mesto { get; set; }
        /// <summary>
        /// Postanski broj adrese
        /// </summary>
        public string PostanskiBroj { get; set; }

    }
}
