using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Models
{
    /// <summary>
    /// DTO za kreiranje adrese
    /// </summary>
    public class AdresaCreateDto
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

        /// <summary>
        /// ID drzave
        /// </summary>
        public Guid DrzavaID { get; set; }
    }
}
