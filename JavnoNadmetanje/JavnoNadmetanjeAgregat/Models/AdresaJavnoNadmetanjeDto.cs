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
        /// Ulica javnog nadmetanja
        /// </summary>
        public string Ulica { get; set; }

        /// <summary>
        /// Broj ulice 
        /// </summary>
        public string Broj { get; set; }

        /// <summary>
        /// Mesto ulice
        /// </summary>
        public string Mesto { get; set; }

        /// <summary>
        /// Postanski broj javnog nadmetanja
        /// </summary>
        public string PostanskiBroj { get; set; }

    }
}
